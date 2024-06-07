using AutoMapper;
using ClassLibrary1BussinesLogic.DBModel;
using PetShop.BusinessLogic;
using PetShop.BusinessLogic.Interfaces;
using PetShop.Domain.Entities.Shop;
using PetShop.Domain.Entities.User;
using PetShop.Domain.Enums;
using PetShop.Web.Attributes;
using PetShop.Web.Extensions;
using PetShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetShop.Web.Controllers
{
    [UserMode]
    public class HomeController : BaseController
    {
        private readonly ISesion _session;
        private readonly IShopping _shopping;
        public HomeController()
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
                if (cartData == null)
                {
                    var createCart = _shopping.CreateCart(userId);
                    if (createCart.Status)
                    {
                        cartData = cartContext.Carts.FirstOrDefault(c => c.UserId == userId);
                    }
                    else
                    {
                        return null;
                    }
                }
                
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




        public ActionResult Index()
        {
        
            using (var db = new ProductContext()) 
            {
                var featuredProducts = db.Products.OrderByDescending(p => p.Orders).Take(8).ToList();
                var newProducts = db.Products.OrderByDescending(p => p.DateCreated).Take(8).ToList();
                var user = GetCurrentUser();
                var cart = GetUserCart(user.Id);

                var homeView = new HomeView
                {
                    CurrentUser = user,
                    UCart = cart,
                    Featured = Mapper.Map<List<ProdDbTable>, List<HomeProduct>>(featuredProducts),
                    NewProducts = Mapper.Map<List<ProdDbTable>, List<HomeProduct>>(newProducts)
                };

                return View(homeView);

            }
        }


        public ActionResult Detail(int productId)
        {
            ProductDetails productDetails;
            List<UserReview> userReviews;
            var user = GetCurrentUser();
            var cart = GetUserCart(user.Id);
            using (var productContext = new ProductContext()) 
            {
                var product = productContext.Products.FirstOrDefault(p => p.Id ==  productId);
                if (product == null) return RedirectToAction("Index");
                productDetails = Mapper.Map<ProductDetails>(product);
            }
            using(var db = new ReviewContext())
            {
                var data = db.Reviews.Where(p => p.ProductId == productId).ToList();
                userReviews = Mapper.Map<List<ReviewDbTable>,List<UserReview>>(data);
            }

            var detailsData = new DetailView
            {
                Details = productDetails,
                Reviews = userReviews,
                UCart = cart,
                CurrentUser = user

            };

            return View(detailsData);
        }


        public ActionResult Terms()
        {
            var user = GetCurrentUser();
            var cart = GetUserCart(user.Id);
            var termsView = new TermsView();
            using (var db = new KnowledgeContext())
            {
                var information = db.Information.FirstOrDefault(i => i.Title == "Terms and Conditions");
                if (information != null)
                {
                    termsView.TermsAndConditions = information.Content;
                }
                else termsView.TermsAndConditions = "";
            }
            termsView.UCart = cart;
            termsView.CurrentUser = user;
            return View(termsView);

        }


    }
} 