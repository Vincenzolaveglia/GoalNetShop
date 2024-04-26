using GoalNetShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoalNetShop.Controllers
{
    public class ProductController : Controller
    {
        private DBContext db = new DBContext();
        public ActionResult Product(int categoryId)
        {
            var category = db.Categories.FirstOrDefault(c => c.CategoryId == categoryId);

            if (category == null)
            {
                return HttpNotFound();
            }

            var products = db.Products.Where(p => p.IdCategory == categoryId).ToList();

            ViewBag.Category = category;
            ViewBag.Category = category;
            return View(products);
        }

        public ActionResult AllProducts()
        {
            var allProducts = db.Products.ToList();

            return View(allProducts);
        }

        public ActionResult Details(int productId)
        {
            var product = db.Products.FirstOrDefault(p => p.ProductId == productId);

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

    }
}