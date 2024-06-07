using PetShop.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetShop.Web.Models
{
    public class HomeView
    {
        public UserMinimal CurrentUser { get; set; }
        public Query Query { get; set; }
        public List<HomeProduct> Featured {  get; set; }
        public List<HomeProduct> NewProducts { get; set; }
        public CartView UCart { get; set; }

    }
}