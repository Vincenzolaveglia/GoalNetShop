using GoalNetShop.Models;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Runtime.Remoting.Messaging;
using System.Data.Entity;

namespace GoalNetShop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        public ActionResult Index()
        {
            DBContext db = new DBContext();

            var placedOrders = db.Orders.Include("Users").ToList();

            return View(placedOrders);
        }

        public ActionResult AddArticle()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddArticle(Product newProduct, HttpPostedFileBase Image, int CategoryId)
        {
            if (ModelState.IsValid)
            {
                if (Image != null && Image.ContentLength > 0)
                {
                    string fileImage = Image.FileName;
                    string pathTosave = Path.Combine(Server.MapPath("~/Content/imgs"), fileImage);
                    Image.SaveAs(pathTosave);

                    newProduct.Image = fileImage;

                }

                newProduct.IdCategory = CategoryId;

                DBContext db = new DBContext();

                db.Products.Add(newProduct);
                db.SaveChanges();

                TempData["AddArticleSuccess"] = "Prodotto aggiunto correttamente";
                return RedirectToAction("AddArticle");
            }

            return View(newProduct);
        }

        public ActionResult EditArticle(int productId)
        {
            using (var db = new DBContext())
            {
                // Recupera il prodotto dal database utilizzando l'ID fornito
                var product = db.Products.FirstOrDefault(p => p.ProductId == productId);
                if (product == null)
                {
                    // Se il prodotto non esiste, reindirizza a una pagina di errore o gestisci di conseguenza
                    return HttpNotFound();
                }

                // Passa il prodotto alla vista per la modifica
                return View(product);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditArticle(Product products, HttpPostedFileBase Image, int CategoryId)
        {
            DbContext db = new DBContext();
            if (Image != null && Image.ContentLength > 0)
            {
                string fileImage = Image.FileName;
                string pathTosave = Path.Combine(Server.MapPath("~/Content/imgs"), fileImage);
                Image.SaveAs(pathTosave);

                products.Image = fileImage;
            }
            if (ModelState.IsValid)
            {
                products.IdCategory = CategoryId;
                db.Entry(products).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AllProducts", "Product");
            }
            return View(products);
        }

        public ActionResult DeleteArticle(int productId)
        {
            using (var db = new DBContext())
            {
                var product = db.Products.Find(productId);
                if (product == null)
                {
                    return HttpNotFound(); 
                }

                db.Products.Remove(product);
                db.SaveChanges();
            }

            return RedirectToAction("AllProducts", "Product");
        }


        public ActionResult OrderDetails(int orderId)
        {
            using (var db = new DBContext())
            {
                // Recupera l'ordine dal database
                var order = db.Orders.Include(o => o.Products).SingleOrDefault(o => o.OrderId == orderId);
                if (order == null)
                {
                    // Se l'ordine non esiste, reindirizza a una pagina di errore o gestisci di conseguenza
                    return HttpNotFound();
                }

                // Passa l'ordine alla vista
                return View(order);
            }
        }


    }
}