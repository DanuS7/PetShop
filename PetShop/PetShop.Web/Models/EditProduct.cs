using PetShop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace PetShop.Web.Models
{
    public class EditProduct
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public ProductCategory Category { get; set; }
        public int AvailableUnits { get; set; }
        public HttpPostedFileBase DefaultImageFile { get; set; }
        public string OldImage { get; set; }
    }
}