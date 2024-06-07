using PetShop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PetShop.Web.Models
{
    public class Product
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public ProductCategory Category { get; set; }
        public int AvailableUnits { get; set; }
        public HttpPostedFileBase DefaultImageFile { get; set; }
    }
}