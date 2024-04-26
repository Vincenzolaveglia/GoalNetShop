using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoalNetShop.Models
{
    public class Cart
    {
        public int ProductId { get; set; }
        public string nameProduct { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Size { get; set; }
    }
}