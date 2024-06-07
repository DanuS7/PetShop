using PetShop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Domain.Entities.Shop
{
    public class ProdDbTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "The Product Name field is required.")]
        [Display(Name = "Product Title")]
        public string Title { get; set; }

        [Display(Name = "Description Price")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The Product Price field is required.")]
        [Display(Name = "Product Price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "The Default Image field is required.")]
        [Display(Name = "Display Image")]
        public byte[] DisplayImage { get; set; }

        [Required(ErrorMessage = "The Product Category field is required.")]
        [Display(Name = "Product Category")]
        public ProductCategory Category { get; set; }

        public bool InStock { get; set; }

        public int AvailableUnits { get; set; }

        public int Orders { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }

        public int? Reviews { get; set; }
        public decimal? Rating { get; set; }


    }
}
