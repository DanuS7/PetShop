using PetShop.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Domain.Entities.Shop
{
    public class ReviewDbTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string Username { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }
        public DateTime DatePosted { get; set; }
    }
}
