using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace PetShop.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/css").Include("~/Template/css/style.css", "~/Template/css/style.min.css"));

            bundles.Add(new StyleBundle("~/css/owlcarousel").Include("~/Template/lib/owlcarousel/assets/owl.carousel.min.css"));

            bundles.Add(new StyleBundle("~/js").Include("~/Template/lib/easing/easing.min.js", "~/Template/lib/owlcarousel/owl.carousel.min.js"));

            bundles.Add(new StyleBundle("~/js/contact").Include("~/Template/mail/jqBootstrapValidation.min.js", "~/Template/mail/contact.js"));

            bundles.Add(new StyleBundle("~/js/template").Include("~/Template/js/main.js"));



        }
    }
}