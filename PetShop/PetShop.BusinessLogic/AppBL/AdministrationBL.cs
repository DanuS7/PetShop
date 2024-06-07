using PetShop.BusinessLogic.Interfaces;
using PetShop.Domain.Entities.Response;
using PetShop.Domain.Entities.Shop;
using PetShop.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.BusinessLogic.AppBL
{
    public class AdministrationBL:AdminApi , IAdministration
    {
        public Response AddProduct(ProductData data)
        {
            return AddProductAction(data);
        }

        public Response EditProduct(EditProductData data) 
        {
            return EditProductAction(data);
        }

        public void DeleteProduct(int id) 
        {
            DeleteProductAction(id);
        }

        public void DeleteReview(int id)
        {
            DeleteReviewAction(id);
        }

        public Response EditTerms(string terms) 
        {
            return EditTermsAction(terms); 
        }

        public Response ChangeOrderStatus(OrderStatusData data)
        {
            return ChangeOrderStatusAction(data);
        }

        public DailyRevenueData GetDailyRevenueData()
        {
            return GetDailyRevenueDataAction();
        }
    }
}
