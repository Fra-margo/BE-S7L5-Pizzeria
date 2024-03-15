using Pizzeria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pizzeria.Controllers
{
    [Authorize(Roles = "admin")]
    public class OrdiniController : Controller
    {
        ModelDBContext db = new ModelDBContext();
        // GET: Ordini
        public ActionResult Index()
        {
            var ordini = db.Ordine.ToList();
            return View(ordini);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateStato(int idOrdine, string nuovoStato)
        {
            var ordine = db.Ordine.Find(idOrdine);
            if (ordine == null)
            {
                return HttpNotFound();
            }

            ordine.Stato = nuovoStato;

            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
