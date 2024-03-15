using Pizzeria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pizzeria.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        ModelDBContext db = new ModelDBContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetNumeroOrdiniEvasi()
        {
            int numeroOrdiniEvasi = db.Ordine.Count(o => o.Stato == "Evaso");
            return Json(numeroOrdiniEvasi, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetIncassoGiornaliero(DateTime data)
        {
            DateTime dataInizio = data.Date;
            DateTime dataFine = data.Date.AddDays(1);

            decimal? totaleIncasso = db.Ordine
                                        .Where(o => o.DataOrdine >= dataInizio && o.DataOrdine < dataFine && o.Stato == "Evaso")
                                        .Sum(o => (decimal?)o.Totale);

            if (totaleIncasso == null)
            {
                totaleIncasso = 0;
            }

            return Json(totaleIncasso, JsonRequestBehavior.AllowGet);
        }
    }
}