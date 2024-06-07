using PetShop.Domain.Entities.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetShop.Web.Models
{
    public class DailyRevenueModel
    {
        public List<HourlyRevenueModel> revenueByHour { get; set; } = new List<HourlyRevenueModel>();
    }
}