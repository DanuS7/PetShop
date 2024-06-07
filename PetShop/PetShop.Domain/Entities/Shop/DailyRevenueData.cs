using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Domain.Entities.Shop
{
    public class DailyRevenueData
    {
        public List<HourlyRevenueData> revenueByHour {  get; set; } = new List<HourlyRevenueData>();
    }
}
