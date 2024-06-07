using PetShop.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetShop.Web.Models
{
    public class ShopView
    {
        public CartView UCart { get; set; }
        public UserMinimal CurrentUser { get; set; }
        public Query Query { get; set; }
        public List<HomeProduct> Products { get; set; }
    }
}