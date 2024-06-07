using PetShop.Domain.Entities.Response;
using PetShop.Domain.Entities.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.BusinessLogic.Interfaces
{
    public interface IShopping
    {
        Response CreateCart(int userId);
        Response AddToCart(AddToCartData item);
        Response RemoveFromCart(RemoveItemCartData item);
        Response ClearCart(ClearCartItemData item);
        List<ProdDbTable> SearchProducts(QueryData query);
        Response PlaceOrder(UCheckoutData data);
        UCheckoutData GetUserBilling(int userId);
        Response AddReview(ReviewData review);

 
    }
}
