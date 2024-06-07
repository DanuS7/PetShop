using AutoMapper;
using Hangfire;
using PetShop.Domain.Entities.Shop;
using PetShop.Domain.Entities.User;
using PetShop.Web.App_Start;
using PetShop.Web.Controllers;
using PetShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace PetShop.Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
           AreaRegistration.RegisterAllAreas();
           RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            InitializeAutoMapper();
            ConfigureHangfire();
        }

        protected static void InitializeAutoMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<UserLogin, ULoginData>();
                cfg.CreateMap<UserRegister, URegisterData>();
                cfg.CreateMap<UDbTable, UserMinimal>();

                cfg.CreateMap<Product, ProductData>();
                cfg.CreateMap<EditProduct, EditProductData>();
                cfg.CreateMap<ProdDbTable, EditProduct>()
                .ForMember(dest => dest.OldImage, opt => opt.MapFrom(src => Convert.ToBase64String(src.DisplayImage)));



                cfg.CreateMap<ProdDbTable, ProductView>()
                .ForMember(dest => dest.DefaultImageBase64, opt => opt.MapFrom(src => Convert.ToBase64String(src.DisplayImage)));
                cfg.CreateMap<ProdDbTable, EditProductView>()
                .ForMember(dest => dest.DefaultImageBase64, opt => opt.MapFrom(src => Convert.ToBase64String(src.DisplayImage)));
                cfg.CreateMap <ProdDbTable, HomeProduct>()
                .ForMember(dest => dest.DefaultImageBase64, opt => opt.MapFrom(src => Convert.ToBase64String(src.DisplayImage)));
                cfg.CreateMap<ProdDbTable, ProductDetails>()
                .ForMember(dest => dest.DefaultImageBase64, opt => opt.MapFrom(src => Convert.ToBase64String(src.DisplayImage)));


                cfg.CreateMap<CartItem, AddToCartData>();
                cfg.CreateMap<CartItem, ClearCartItemData>(); 
                cfg.CreateMap<CartDbTable, CartView>()
                .ForMember(dest => dest.Items, opt => opt.Ignore());
                cfg.CreateMap<CartItemData, CartItemView>()
               .ForMember(dest => dest.DefaultImageFile, opt => opt.MapFrom(src => Convert.ToBase64String(src.DisplayImage)));

                cfg.CreateMap<Query, QueryData>();
                cfg.CreateMap<UCheckoutData, UserCheckout>();
                cfg.CreateMap<CartItemData, OrderItemDb>();

                cfg.CreateMap<OrderView, OrderDbTable>();
                cfg.CreateMap<OrderDetails, OrderDbTable>();
                cfg.CreateMap<OrderChangeStatus, OrderStatusData>();
                cfg.CreateMap<ReviewData, UserReview>();
                cfg.CreateMap<ReviewDbTable, UserReview>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Username));
                cfg.CreateMap<ReviewDbTable, AdminReviewView>();


                cfg.CreateMap<HourlyRevenueData, HourlyRevenueModel>();
                cfg.CreateMap<DailyRevenueData, DailyRevenueModel>();

            });

        }

        protected static void ConfigureHangfire()
        {

            GlobalConfiguration.Configuration.UseSqlServerStorage("petShop");

            Hangfire.BackgroundJobServerOptions serverOptions = new Hangfire.BackgroundJobServerOptions();
            Hangfire.BackgroundJobServer server = new Hangfire.BackgroundJobServer(serverOptions);

            RecurringJob.AddOrUpdate<MaintenanceController>("CleanupGuestUsers",
              x => x.CleanupGuestUsers(),
                   "* * * * *");
        }
    }
}