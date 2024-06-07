using PetShop.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Domain.Entities.Shop
{
    public class RemoveItemCartData
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public UserMinimal user { get; set; }
    }
}
