using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetShop.Web.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ItemsCount { get; set; }
        public List<CartItemView> Items { get; set; }
        public decimal Total { get; set;  }
    }
}