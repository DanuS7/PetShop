using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace PetShop.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //User Resources 
            bundles.Add(new StyleBundle("~/css").Include("~/WebTemplate/lib/owlcarousel/assets/owl.carousel.min.css", "~/WebTemplate/css/style.css"));

            bundles.Add(new ScriptBundle("~/js").Include("~/WebTemplate/js/main.js"));
            bundles.Add(new ScriptBundle("~/js/contact").Include("~/WebTemplate/mail/jqBootstrapValidation.min.js", "~/WebTemplate/mail/contact.js"));
            bundles.Add(new ScriptBundle("~/js/owl").Include("~/WebTemplate/lib/easing/easing.min.js", "~/WebTemplate/lib/owlcarousel/owl.carousel.min.js"));

            //Admin Resources
            bundles.Add(new StyleBundle("~/css/admin").Include(
                "~/AdminTemplate/assets/vendor/bootstrap/css/bootstrap.min.css",
                "~/AdminTemplate/assets/vendor/fonts/circular-std/style.css",
                "~/AdminTemplate/assets/libs/css/style.css",
                "~/AdminTemplate/assets/vendor/fonts/fontawesome/css/fontawesome-all.css",
                "~/AdminTemplate/assets/vendor/charts/chartist-bundle/chartist.css",
                "~/AdminTemplate/assets/vendor/charts/morris-bundle/morris.css",
                "~/AdminTemplate/assets/vendor/fonts/material-design-iconic-font/css/materialdesignicons.min.css",
                "~/AdminTemplate/assets/vendor/charts/c3charts/c3.css",
                "~/AdminTemplate/assets/vendor/fonts/flag-icon-css/flag-icon.min.css"
                ));

            bundles.Add(new ScriptBundle("~/js/admin").Include(
                "~/AdminTemplate/assets/vendor/jquery/jquery-3.3.1.min.js",
                "~/AdminTemplate/assets/vendor/bootstrap/js/bootstrap.bundle.js",
                "~/AdminTemplate/assets/vendor/slimscroll/jquery.slimscroll.js",
                "~/AdminTemplate/assets/libs/js/main-js.js",
                "~/AdminTemplate/assets/vendor/charts/chartist-bundle/chartist.min.js",
                "~/AdminTemplate/assets/vendor/charts/sparkline/jquery.sparkline.js",
                "~/AdminTemplate/assets/vendor/charts/morris-bundle/raphael.min.js",
                "~/AdminTemplate/assets/vendor/charts/morris-bundle/morris.js",
                "~/AdminTemplate/assets/vendor/charts/c3charts/c3.min.js",
                "~/AdminTemplate/assets/vendor/charts/c3charts/d3-5.4.0.min.js",
                "~/AdminTemplate/assets/vendor/charts/c3charts/C3chartjs.js",
                "~/AdminTemplate/assets/libs/js/dashboard-ecommerce.js"
                ));


            //Datatable Bundle
            bundles.Add(new StyleBundle("~/css/admin/datatable").Include(
            "~/AdminTemplate/assets / vendor / datatables / css / dataTables.bootstrap4.css",
            "~/AdminTemplate/assets/vendor/datatables/css/buttons.bootstrap4.css",
            "~/AdminTemplate/assets/vendor/datatables/css/select.bootstrap4.css",
            "~/AdminTemplate/assets/vendor/datatables/css/fixedHeader.bootstrap4.css"
                ));

            bundles.Add(new ScriptBundle("~/js/admin/datatable").Include(
                "~/AdminTemplate/assets/vendor/multi-select/js/jquery.multi-select.js",
                "~/AdminTemplate/assets/vendor/datatables/js/dataTables.bootstrap4.min.js",
                "~/AdminTemplate/assets/vendor/datatables/js/buttons.bootstrap4.min.js",
                "~/AdminTemplate/assets/vendor/datatables/js/data-table.js"
                ));

        }
    }
}