using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetShop.Web.Models
{
    public class ProductDetails
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string DefaultImageBase64 { get; set; }
        public bool InStock { get; set; }
        public string Description { get; set; }
        public int? Reviews { get; set; }
        public decimal? Rating { get; set; }
    }
}