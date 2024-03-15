using Pizzeria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Pizzeria.Controllers
{
    public class AuthController : Controller
    {
        ModelDBContext db = new ModelDBContext();
        // GET: Auth
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
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Utenti user)
        {
            var loggedUser = db.Utenti.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
            if (loggedUser == null)
            {
                TempData["ErrorLogin"] = "Credenziali non valide.";
                return RedirectToAction("Login");
            }

            FormsAuthentication.SetAuthCookie(loggedUser.Username.ToString(), true);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Utenti user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = db.Utenti.FirstOrDefault(u => u.Username == user.Username);
                if (existingUser != null)
                {
                    TempData["ErrorRegister"] = "Username già esistente.";
                    return RedirectToAction("Register");
                }

                db.Utenti.Add(user);
                db.SaveChanges();

                FormsAuthentication.SetAuthCookie(user.Username.ToString(), true);
                return RedirectToAction("Login", "Auth");
            }

            return View(user);
        }
    }
}