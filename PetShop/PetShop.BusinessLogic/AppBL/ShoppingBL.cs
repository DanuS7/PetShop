using PetShop.BusinessLogic.Interfaces;
using PetShop.Domain.Entities.Response;
using PetShop.Domain.Entities.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.BusinessLogic.AppBL
{
    public class ShoppingBL:ShopApi, IShopping
    {
        public Response CreateCart(int userId)
        {
            return CreateCartAction(userId);
        }

        public Response AddToCart(AddToCartData item)
        {
            return AddToCartAction(item);
        }

       public Response ClearCart(ClearCartItemData item)
        { 
            return ClearCartAction(item);
        }

        public Response RemoveFromCart(RemoveItemCartData item)
        { 
            return RemoveFromCartAction(item);
        }

        public List<ProdDbTable> SearchProducts(QueryData query)
        {
            return SearchProductsAction(query);
        }

        public Response PlaceOrder(UCheckoutData data)
        {
            return PlaceOrderAction(data);
        }

        public UCheckoutData GetUserBilling(int userId)
        {
            return GetUserBillinngAction(userId);
        }

        public Response AddReview(ReviewData review)
        {
            return AddReviewAction(review);
        }
    }
}
