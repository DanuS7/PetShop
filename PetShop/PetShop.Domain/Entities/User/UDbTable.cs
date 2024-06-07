using PetShop.Domain.Entities.Shop;
using PetShop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PetShop.Domain.Entities.User
{

    public class UDbTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string Username { get; set; }

        [Display(Name = "Surname")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Surname cannot be longer than 50 characters.")]
        public string Surname { get; set; }

        [Display(Name = "Password")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password cannot be shorter than 8 characters")]
        public string Password { get; set; }

        [Display(Name = "Email Address")]
        [StringLength(30)]
        public string Email { get; set; }

        [Display(Name = "Address")]
        [StringLength(100)]
        public string Address { get; set; }

        [Display(Name = "Country")]
        [StringLength(30)]
        public string Country { get; set; }

        [Display(Name = "State")]
        [StringLength(50)]
        public string State { get; set; }

        [Display(Name = "City")]
        [StringLength(50)]
        public string City { get; set; }

        [Display(Name = "Zip Code")]
        [StringLength(10)]
        public string ZipCode { get; set; }

        [Display(Name = "Phone Number")]
        [StringLength(10)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Profile Picture")]
        public byte[] ProfilePicture { get; set; }

        [DataType(DataType.Date)]
        public DateTime LastLogin { get; set; }

        [StringLength(30)]
        public string LastIP { get; set; }

        public URole Level { get; set; }
        public virtual CartDbTable Carts { get; set; }
    }
}