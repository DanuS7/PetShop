using PetShop.Domain.Entities.Shop;
using PetShop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetShop.Web.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Total { get; set; }
        public DateTime OrderDate { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public OrderStatus Status { get; set; }
        public virtual IEnumerable<OrderItemDb> Items { get; set; }
    }
}