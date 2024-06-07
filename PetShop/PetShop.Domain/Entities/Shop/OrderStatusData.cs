using PetShop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Domain.Entities.Shop
{
    public class OrderStatusData
    {
        public int OrderId { get; set; }
        public OrderStatus status { get; set; }
    }
}
