using PetShop.BusinessLogic.Interfaces;
using PetShop.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetShop.Web.Models;
using AutoMapper;
using PetShop.Domain.Entities.Shop;
using System.ServiceModel.Channels;
using ClassLibrary1BussinesLogic.DBModel;
using PetShop.Domain.Entities.User;
using PetShop.Web.Extensions;
using PetShop.Domain.Enums;
using PetShop.Web.Attributes;

namespace PetShop.Web.Controllers
{
    [UserMode]
    public class ShoppingController : BaseController
    {


        private readonly ISesion _session;
        private readonly IShopping _shopping;
        public ShoppingController()
        {
            var bl = new BussinessLogic();
            _session = bl.GetSessionBL();
            _shopping = bl.GetShopping();
        }

        public UserMinimal GetCurrentUser()
        {
        
            return System.Web.HttpContext.Current.GetMySessionObject();
        }

        public CartView GetUserCart(int userId)
        {
            using (var cartContext = new CartContext())
            {
                var cartData = cartContext.Carts.FirstOrDefault(c => c.UserId == userId);
                if(cartData != null) 
                {
                    var cartView = Mapper.Map<CartView>(cartData);
                    var itemsView = new List<CartItemView>();
                    foreach (var product in cartData.Items)
                    {
                        itemsView.Add(Mapper.Map<CartItemView>(product));
                    }
                    cartView.Items = itemsView;
                    return cartView;
                }
                
            }
            return null;
        }

        public ActionResult Cart()
        {
            var currentUser = GetCurrentUser();
            var cart = GetUserCart(currentUser.Id);

            if (cart == null)
            {
                var createCart = _shopping.CreateCart(currentUser.Id);
                if (!createCart.Status) return RedirectToAction("Index", "Login");
                cart = GetUserCart(currentUser.Id);
            }
            cart.CurrentUser = currentUser;

            return View(cart);
        }

        [HttpPost]
        public ActionResult AddToCart( CartItem item) 
        {
          
            var data = Mapper.Map<AddToCartData>(item);
            var currentUser = GetCurrentUser();
            data.user = currentUser;
            
            var addToCart = _shopping.AddToCart(data);

            if(addToCart.Status)
            {     
                var cartView = GetUserCart(currentUser.Id);
                if(cartView == null) return Json(new { success = false, Message = "Cart Not Found"});

                return Json(new { success = true, cartView });

            }
            else return Json(new { success = false, message = addToCart.ActionStatusMsg });
        
        }

        [HttpPost]
        public ActionResult RemoveFromCart( CartItem item) 
        {
       
            var data = new RemoveItemCartData
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
            };
            var currentUser = GetCurrentUser();
            data.user = currentUser;

            var removeFromCart = _shopping.RemoveFromCart(data);

            if (removeFromCart.Status)
            {
                var cartView = GetUserCart(currentUser.Id);
                if (cartView == null) return Json(new { success = false, Message = "Cart Not Found" });
                return Json(new { success = true, cartView });

            }
            else return Json(new { success = false, Message = removeFromCart.ActionStatusMsg });
        }


        [HttpPost]
        public ActionResult ClearItem(CartItem item) 
        {
    
            var data = Mapper.Map<ClearCartItemData>(item);
            var currentUser = GetCurrentUser();
            data.user = currentUser;
            var clearCart = _shopping.ClearCart(data);

            if (clearCart.Status)
            {
                var cartView = GetUserCart(currentUser.Id);
                if (cartView == null) return Json(new { success = false, Message = "Cart Not Found" });
                return Json(new { success = true, cartView });

            }
            else return Json(new { success = false, Message = clearCart.ActionStatusMsg });
        }


        public ActionResult Shop(ProductCategory? category)
        {
            var user = GetCurrentUser();
            var cart = GetUserCart(user.Id);
            var query = new Query 
            { 
              Category = category,
              MaxPrice = null,
              UserQuery = null,
              MinPrice = null,
              SortByType = null,
            };
            var data = Mapper.Map<QueryData>(query);
            var productsData = _shopping.SearchProducts(data);
            var productsList = Mapper.Map<List<ProdDbTable>, List<HomeProduct>>(productsData);
            var shopView = new ShopView
            {
                Query = query,
                Products = productsList,
                UCart = cart,
                CurrentUser = user,
            };
            return View(shopView);
        }



        [HttpPost]
        public ActionResult Shop(ShopView data)
        {
            var user = GetCurrentUser();
            var cart = GetUserCart(user.Id);
            var query = Mapper.Map<QueryData>(data.Query);
            var productsData = _shopping.SearchProducts(query);
            var productsList = Mapper.Map<List<ProdDbTable>, List<HomeProduct>>(productsData);
            var shopView = new ShopView
            {
                Products = productsList,
                Query = data.Query,
                UCart = cart,
                CurrentUser = user,
            };

            return View(shopView);
        }


        public ActionResult Checkout()
        {

            var currentUser = GetCurrentUser();
            var cart = GetUserCart(currentUser.Id);
            if (currentUser != null)
            {
                var data = _shopping.GetUserBilling(currentUser.Id);
                var billingDetail = Mapper.Map<UserCheckout>(data);
                var viewData = new CheckoutView
                {
                    Checkout = billingDetail,
                    UCart = cart,
                    CurrentUser = currentUser,
                };
                return View(viewData);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(CheckoutView data)
        {
            if(ModelState.IsValid)
            {
                var details = Mapper.Map<UCheckoutData>(data.Checkout);
                var placeOrder = _shopping.PlaceOrder(details);
                if(placeOrder.Status)
                {
                    return RedirectToAction("Index", "Home");
                }
                return View();
            }
            return View();
        }


        [HttpPost]
        public ActionResult AddReview(UserReview reviewData)
        {
            var currentUser = GetCurrentUser();
            reviewData.UserId = currentUser.Id;
            var review = Mapper.Map<ReviewData>(reviewData);
            var addReview = _shopping.AddReview(review);
            if(addReview.Status)
            {
                return Json(new { success = true, message = "Review added successfully" });
            }
            else
            {
                return Json(new { success = false, message = "An error occurred while adding the review" });
            }
        }

    }
}