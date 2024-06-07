using PetShop.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetShop.Web.Models
{
    public class DetailView
    {
        public UserMinimal CurrentUser { get; set; }
        public CartView UCart { get; set; }
        public Query Query { get; set; }
        public ProductDetails Details { get; set; }
        public List<UserReview> Reviews { get; set; }
    }
}