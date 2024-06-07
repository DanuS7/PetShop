using PetShop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PetShop.Domain.Entities.Shop
{
    public class EditProductData
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public HttpPostedFileBase DefaultImageFile { get; set; }
        public ProductCategory Category { get; set; }
        public int AvailableUnits { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
