using GoalNetShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoalNetShop.Controllers
{
    public class ShopController : Controller
    {
        [HttpGet]
        public ActionResult Cart()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cart(string nameProduct, decimal price, string size, int quantity)
        {
            List<Cart> cart = Session["Cart"] as List<Cart>;

            if (cart == null)
            {
                cart = new List<Cart>();
                Session["Cart"] = cart;
            }

            // Verifica se il prodotto è già nel carrello
            var existingItem = cart.FirstOrDefault(item => item.nameProduct == nameProduct && item.Size == size);
            if (existingItem != null)
            {
                // Se il prodotto è già presente nel carrello, aggiorna la quantità
                existingItem.Quantity += quantity;
            }
            else
            {
                // Se il prodotto non è nel carrello, aggiungilo
                cart.Add(new Cart { nameProduct = nameProduct, Price = price, Size = size, Quantity = quantity });
            }

            return RedirectToAction("Cart");
        }


        [HttpPost]
        public ActionResult EmptyCart()
        {
            // Svuota il carrello eliminando le voci di carrello dalla sessione
            Session["Cart"] = null;

            return RedirectToAction("Cart");
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int productId)
        {
            // Recupera il carrello dalla sessione
            List<Cart> cart = Session["Cart"] as List<Cart>;

            if(cart != null)
            {
                // Cerca il prodotto nel carrello utilizzando l'Id
                var productToremove = cart.FirstOrDefault(item => item.ProductId == productId);

                if(productToremove != null)
                {
                    // Rimuovi il prodotto dal carrello
                    cart.Remove(productToremove);
                    // Aggiorna la sessione con il carrello aggiornato
                    Session["Cart"] = cart;
                }
            }
            return RedirectToAction("Cart");
        }

        [HttpGet]
        public ActionResult Order()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Order(Order newOrder)
        {
            // Verifica se l'utente è loggato
            if (!User.Identity.IsAuthenticated)
            {
                // Se l'utente non è loggato, reindirizza alla pagina di accesso
                return RedirectToAction("Login", "Auth");
            }

            // Recupera il carrello dalla sessione
            List<Cart> cart = Session["Cart"] as List<Cart>;

            if (cart != null)
            {
                // Calcola la quantità totale e il totale dell'ordine dal carrello
                int totalQuantity = cart.Sum(item => item.Quantity);
                decimal totalPrice = cart.Sum(item => item.Price * item.Quantity);

                // Assegna la quantità totale e il totale dell'ordine all'oggetto Order
                newOrder.Quantity = totalQuantity;
                newOrder.Total = totalPrice;

            }

            if (ModelState.IsValid)
            {
                DBContext db = new DBContext();

                newOrder.UserId = Convert.ToInt32(User.Identity.Name);
                newOrder.OrderDate = DateTime.Now;

                db.Orders.Add(newOrder);
                db.SaveChanges();

                return RedirectToAction("Success", new { orderId = newOrder.OrderId });
            }

            return View(newOrder);
        }




        public ActionResult Success()
        {
            //Session.Remove("Cart");
            return View();
        }


    }

}
