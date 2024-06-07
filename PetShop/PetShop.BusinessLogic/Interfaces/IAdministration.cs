using PetShop.Domain.Entities.Response;
using PetShop.Domain.Entities.Shop;
using PetShop.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.BusinessLogic.Interfaces
{
    public interface IAdministration
    {
        Response AddProduct(ProductData data);
        Response EditProduct(EditProductData data);
        void DeleteProduct(int id);
        void DeleteReview(int id);
        Response EditTerms(string terms);
        Response ChangeOrderStatus(OrderStatusData data);
        DailyRevenueData GetDailyRevenueData();
    }
}
