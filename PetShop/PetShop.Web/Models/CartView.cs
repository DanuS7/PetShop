using PetShop.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetShop.Web.Models
{
    public class CartView
    {
        public CartView UCart { get; set; }
        public Query Query { get; set; }
        public UserMinimal CurrentUser { get; set; }
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<CartItemView> Items { get; set; }
        public int TotalItems { get; set; }
        public decimal TotalPrice { get; set; }
    }
}