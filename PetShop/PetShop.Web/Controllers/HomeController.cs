using PetShop.BusinessLogic;
using PetShop.BusinessLogic.Interfaces;
using PetShop.Web.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetShop.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ISesion _session;
        public HomeController()
        {
            var bl = new BussinessLogic();
            _session = bl.GetSessionBL();
        }

        public ActionResult Index()
        {
            SessionStatus();
            string userStatus = (string)System.Web.HttpContext.Current.Session["LoginStatus"];
            if (userStatus != "login")
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        [AdminMode]
        public ActionResult Shop()
        {
            return View();
        }
    }
}