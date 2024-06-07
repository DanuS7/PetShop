using PetShop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetShop.Web.Models
{
    public class HomeProduct
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public bool InStock { get; set; }
        public string DefaultImageBase64 { get; set; }
        

    }
}