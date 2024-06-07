using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetShop.Domain.Enums;

namespace PetShop.Web.Models
{
    public class OrderChangeStatus
    {
        public int OrderId { get; set; }
        public OrderStatus status { get; set; }
    }
}