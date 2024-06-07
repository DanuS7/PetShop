using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetShop.Web.Models
{
    public class AdminReviewView
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ProductTitle { get; set; }
        public string Username { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public DateTime DatePosted { get; set; }
        public string ProductImage { get; set; }
    }
}