using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetShop.Domain.Entities.User;

namespace PetShop.Domain.Entities.Shop
{
    public class CartDbTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "User field is required.")]
        [Display(Name = "User Id")]
        public int UserId { get; set; }

        public int TotalItems { get; set; }
        public decimal TotalPrice { get; set; }

        [Display(Name = "Items")]
        public virtual ICollection<CartItemData> Items { get; set; }


    }
}
