using PetShop.BusinessLogic.Interfaces;
using PetShop.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetShop.Web.Models;
using AutoMapper;
using Microsoft.Win32;
using PetShop.Domain.Entities.User;
using PetShop.Domain.Entities.Shop;
using ClassLibrary1BussinesLogic.DBModel;
using PetShop.Web.Attributes;

namespace PetShop.Web.Controllers
{
    [AdminMode]
    public class AdminController : BaseController
    {

        private readonly ISesion _session;
        private readonly IAdministration _administration;
        public AdminController()
        {
            var bl = new BussinessLogic();
            _session = bl.GetSessionBL();
            _administration = bl.GetAdministrationBL();
        }




        public ActionResult Dashboard()
        {
            var dashboardView = new AdminDashboardView();
            var data = _administration.GetDailyRevenueData();
            var dailyRevenue = Mapper.Map<DailyRevenueModel>(data);
            dashboardView.DailyRevenue = dailyRevenue;
            
            return View(dashboardView);
        }

        public ActionResult Products()
        {
            List<ProductView> productsViewModel;

            using (var db = new ProductContext())
            {
                var products = db.Products.OrderByDescending(p => p.DateCreated).ToList();
                productsViewModel = Mapper.Map<List<ProdDbTable>, List<ProductView>>(products);
            }
            ViewBag.Products = productsViewModel;

            return View();
        }

        public ActionResult AddProduct()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
              
                var data = Mapper.Map<ProductData>(product);
                data.CreateDate = DateTime.Now;
                

                var adminBl = new BussinessLogic();
                var addProduct = _administration.AddProduct(data);

                if (addProduct.Status)
                {
                    return RedirectToAction("Products", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", addProduct.ActionStatusMsg);
                    return View();
                }
            }

            return View(product);
        }


        public ActionResult EditProduct(int id)
        {
            using (var db = new ProductContext()) 
            {
                var data = db.Products.FirstOrDefault(p => p.Id == id);
                var product = Mapper.Map<ProdDbTable,EditProduct>(data);
                if(product != null)
                {
                    return View(product);
                }
                return HttpNotFound();
            } 
        }

       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(EditProduct product)
        {
            if (ModelState.IsValid)
            {
                var data = Mapper.Map<EditProductData>(product);

                var adminBl = new BussinessLogic();
                var editProduct = _administration.EditProduct(data);
                if (editProduct.Status) 
                {
                    return RedirectToAction("Products", "Admin");
                }
                else
                {
                    ModelState.AddModelError("",editProduct.ActionStatusMsg);
                    return View();
                }
            }
            return View(product);
        }

        public ActionResult DeleteProduct(int id)
        {
            _administration.DeleteProduct(id);
            return RedirectToAction("Products", "Admin");
        }


        public ActionResult Orders()
        {
            
            using (var db = new OrderContext())
            {
                var data = db.Orders.ToList();
                var orders = Mapper.Map<List<OrderDbTable>, List<OrderView>>(data);
                return View(orders);
            }   
        }


        public ActionResult OrderManage(int orderId)
        { 
            using (var db = new OrderContext())
            {
                var data = db.Orders.FirstOrDefault(o => o.Id == orderId);
                var order = Mapper.Map<OrderDetails>(data);
                return View(order);
            }
        }

        [HttpPost]
        public ActionResult ChangeOrderStatus(OrderChangeStatus data)
        {
            var status = Mapper.Map<OrderStatusData>(data);
            var response = _administration.ChangeOrderStatus(status);
            if(response.Status)
            {
                return RedirectToAction("OrderManage", new { orderId = data.OrderId });
            }
            else 
            {
                ModelState.AddModelError("", response.ActionStatusMsg);
                return RedirectToAction("Dashboard");
            }
        }

        public ActionResult Reviews()
        {
            List<AdminReviewView> reviews;
            using (var db = new ReviewContext())
            {
                var data = db.Reviews.OrderByDescending(r => r.DatePosted).ToList();
                reviews = Mapper.Map<List<ReviewDbTable>, List<AdminReviewView>>(data);
                using(var productDb = new ProductContext())
                {
                    foreach(var review in reviews)
                    {
                        var product = productDb.Products.FirstOrDefault(p => p.Id == review.ProductId);
                        if(product != null)
                        {
                            review.ProductImage = Convert.ToBase64String(product.DisplayImage);
                        }
                    }
                } 
            }
            return View(reviews);
        }

        public ActionResult DeleteReview(int id)
        {
            _administration.DeleteReview(id);
            return RedirectToAction("Reviews");
        }



        public ActionResult EditTerms()
        {
            EditTermsModel model = new EditTermsModel();
            using (var db = new KnowledgeContext()) 
            {
                var terms = db.Information.FirstOrDefault(i => i.Title == "Terms and Conditions");
                if (terms != null)
                {
                    model.TermsAndConditions = terms.Content;
                    return View(model);
                }
                model.TermsAndConditions = null;
            }
            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTerms(EditTermsModel data)
        {
            if(ModelState.IsValid)
            {
                var response = _administration.EditTerms(data.TermsAndConditions);
                if (response.Status)
                {
                    return RedirectToAction("Dashboard","Admin");
                }
                else
                {
                    ModelState.AddModelError("", response.ActionStatusMsg);
                    return View();
                }
            }
            return View();

        }


    }
}