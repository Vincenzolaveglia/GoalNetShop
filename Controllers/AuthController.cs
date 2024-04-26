using GoalNetShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GoalNetShop.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Name, Email, Password")] User user)
        {
            if (ModelState.IsValid)
            {
                DBContext db = new DBContext();
                var registeredUser = db.Users
                    .Where(u => u.Name == user.Name && u.Email == user.Email && u.Password == user.Password)
                    .FirstOrDefault();

                if (registeredUser != null)
                {
                    FormsAuthentication.SetAuthCookie(registeredUser.UserId.ToString(), true);
                    switch (registeredUser.RoleId)
                    {
                        case 1: // admin
                  
                            return RedirectToAction("Index", "Admin");

                        case 2: // utenti
                 
                            return RedirectToAction("Index", "Home");

                            // altri ruoli eventuali
                    }
                }
                else
                {
                    TempData["IsValid"] = "Credenziali Errate";
                    return RedirectToAction("Login");
                }

                return RedirectToAction("Index", "Home");

            }

            return View(user);
        }


        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                DBContext db = new DBContext();

                // Verifica se l'email o il nome sono già presenti nel database
                if (db.Users.Any(u => u.Email == user.Email || u.Name == user.Name))
                {
                    if (db.Users.Any(u => u.Email == user.Email))
                    {
                        ModelState.AddModelError("Email", "L'indirizzo email è già stato registrato.");
                    }
                    if (db.Users.Any(u => u.Name == user.Name))
                    {
                        ModelState.AddModelError("Name", "Il nome utente è già stato registrato.");
                    }
                    return View(user);
                }

                db.Users.Add(user);
                db.SaveChanges();

                return RedirectToAction("Login", "Auth");
            }
            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}