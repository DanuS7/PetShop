using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Domain.Enums
{
    public enum SortBy
    {
        [Display(Name = "Most Popular")]
        MostPopular = 0,

        [Display(Name = "Least Popular")]
        LeastPopular = 1,

        [Display(Name = "Price Ascending")]
        PriceAscending = 2,

        [Display(Name = "Price Descending")]
        PriceDescending = 3,

        [Display(Name = "New Products")]
        NewProducts = 4,

        [Display(Name = "Old Products")]
        OldProducts = 5,
    }
}
