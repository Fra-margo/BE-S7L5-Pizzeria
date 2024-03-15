using Pizzeria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Pizzeria.Controllers
{
    public class HomeController : Controller
    {
        ModelDBContext db = new ModelDBContext();
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("admin"))
                {
                    return RedirectToAction("IndexAdmin", "Home");
                }
                else
                {
                    return RedirectToAction("IndexUser", "Home");
                }
            }
            ViewBag.Title = "Home Page";

            return View("Index");
        }

        [Authorize(Roles = "admin")]
        public ActionResult IndexAdmin()
        {
            return View();
        }
        [Authorize]
        public ActionResult IndexUser()
        {
            var prodotti = db.Prodotti.ToList();
            ViewBag.Ingredienti = db.Ingredienti.ToList();
            return View(prodotti);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AggiungiAllOrdine(int idProdotto, int quantita, int?[] ingredientiSelezionati)
        {
            var prodotto = db.Prodotti.FirstOrDefault(p => p.IdProdotto == idProdotto);

            if (prodotto == null)
            {
                return HttpNotFound();
            }

            var ingredienti = new List<Ingredienti>();
            if (ingredientiSelezionati != null)
            {
                foreach (var idIngrediente in ingredientiSelezionati)
                {
                    var ingrediente = db.Ingredienti.FirstOrDefault(i => i.IdIngrediente == idIngrediente);
                    if (ingrediente != null)
                    {
                        ingredienti.Add(ingrediente);
                    }
                }
            }
            var ordine = new OrdineModel
            {
                NomeProdotto = prodotto.Nome,
                PrezzoUnitario = prodotto.Prezzo,
                Quantita = quantita,
                IngredientiSelezionati = ingredienti
            };

            var ordini = Session["Ordini"] as List<OrdineModel> ?? new List<OrdineModel>();
            ordini.Add(ordine);
            Session["Ordini"] = ordini;

            return RedirectToAction("IndexUser");
        }

        [Authorize]
        public ActionResult RiepilogoOrdine(int? quantita)
        {
            var ordini = Session["Ordini"] as List<OrdineModel>;

            if (ordini == null || ordini.Count == 0)
            {
                return RedirectToAction("IndexUser");
            }

            decimal totale = ordini.Sum(o => o.PrezzoUnitario * o.Quantita);

            if (quantita.HasValue && quantita.Value > 0)
            {
                totale *= quantita.Value;
            }

            ViewBag.Totale = totale;

            return View(ordini);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RimuoviDalOrdine(string nomeProdotto)
        {
            var ordini = Session["Ordini"] as List<OrdineModel>;

            if (ordini != null)
            {
                var prodottoDaRimuovere = ordini.FirstOrDefault(o => o.NomeProdotto == nomeProdotto);
                if (prodottoDaRimuovere != null)
                {
                    ordini.Remove(prodottoDaRimuovere);
                    Session["Ordini"] = ordini;
                }
            }

            if (ordini != null && ordini.Count > 0)
            {
                return RedirectToAction("RiepilogoOrdine", "Home");
            }
            else
            {
                return RedirectToAction("IndexUser", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfermaOrdine(string indirizzo, string note)
        {
            var ordini = Session["Ordini"] as List<OrdineModel>;

            if (ordini == null || ordini.Count == 0 || !User.Identity.IsAuthenticated)
            {
                return RedirectToAction("IndexUser");
            }

            decimal totale = ordini.Sum(o => o.PrezzoUnitario * o.Quantita);

            var nuovoOrdine = new Ordine
            {
                Username = User.Identity.Name, 
                Indirizzo = indirizzo,
                Note = note,
                Stato = "In attesa",
                Totale = totale
            };

            db.Ordine.Add(nuovoOrdine);
            db.SaveChanges();


            Session.Remove("Ordini");

            return RedirectToAction("IndexUser");
        }
    }
}
