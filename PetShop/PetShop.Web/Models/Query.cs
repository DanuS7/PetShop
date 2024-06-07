using PetShop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetShop.Web.Models
{
    public class Query
    {
        public string UserQuery { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public ProductCategory? Category { get; set; }
        public SortBy? SortByType { get; set; }
    }
}