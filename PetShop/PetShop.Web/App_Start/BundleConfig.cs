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
            bundles.Add(new StyleBundle("~/css").Include("~/css/bootstrap.css", "~/css/style.css", "~/css/responsive.css"));

            bundles.Add(new ScriptBundle("~/js").Include("~/js/jquery-3.4.1.min.js", "~/js/bootstrap.js", "~/js/custom.js"));
            

        }
    }
}