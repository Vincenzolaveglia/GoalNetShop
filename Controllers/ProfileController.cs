using GoalNetShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoalNetShop.Controllers
{
    public class ProfileController : Controller
    {
        [HttpGet]
        [Authorize]
        public ActionResult Profile()
        {
            // Recupera l'ID dell'utente autenticato
            int userId = int.Parse(User.Identity.Name);

            using (var db = new DBContext())
            {
                // Trova l'utente nel database e include anche gli ordini associati
                var user = db.Users
                              .Where(u => u.UserId == userId)
                              .Include(u => u.Orders)
                              .FirstOrDefault();

                if (user != null)
                {
                    return View(user);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult EditProfile()
        {
            // Recupera l'ID dell'utente autenticato
            int userId = int.Parse(User.Identity.Name);

            using (var db = new DBContext())
            {
                // Trova l'utente nel database
                var user = db.Users.FirstOrDefault(u => u.UserId == userId);

                if (user != null)
                {
                    return View(user);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(User user)
        {
            if (ModelState.IsValid)
            {
                using (var db = new DBContext())
                {
                    // Trova l'utente nel database
                    var editUser = db.Users.FirstOrDefault(u => u.UserId == user.UserId);

                    if (editUser != null)
                    {
                        // Aggiorna i dettagli dell'utente con i nuovi valori
                        editUser.Name = user.Name;
                        editUser.Email = user.Email;
                        editUser.Password = user.Password;

                        // Salva le modifiche nel database
                        db.SaveChanges();

                        // Reindirizza all'azione di visualizzazione del profilo
                        return RedirectToAction("Profile");
                    }
                    else
                    {
                        return HttpNotFound();
                    }
                }
            }

            // Se il modello non è valido, ritorna alla vista di modifica con gli errori di validazione
            return View(user);
        }
    }
}





