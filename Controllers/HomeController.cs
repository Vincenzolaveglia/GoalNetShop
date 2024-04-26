using GoalNetShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoalNetShop.Controllers
{
    public class HomeController : Controller
    {
        private DBContext db = new DBContext();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            List<Category> categories = db.Categories.ToList();
            return View(categories);
        }

        [HttpGet]
        public ActionResult Faq()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

    }
}
