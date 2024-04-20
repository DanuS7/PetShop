using PetShop.BusinessLogic;
using PetShop.BusinessLogic.Interfaces;
using PetShop.Domain.Entities.User;
using PetShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace PetShop.Web.Controllers
{
    public class LoginController : Controller
    {

        private readonly ISesion _auth;
        // GET: Login
        public LoginController()
        {
            var bl = new BussinesLogic();
            _auth = bl.GetSessionBL();
        } 
        
        
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UserLogin login)
        {
            if (ModelState.IsValid)
            {
                ULoginData data = new ULoginData
                {

                    Credential = login.Credential,
                    Password = login.Password,
                    UserIp = Request.UserHostAddress,
                    LoginDate = DateTime.Now
                };
                var userLogin = _auth.UserLogin(data);
                if (userLogin.Status)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", userLogin.ActionStatusMsg);
                    return View();
                }
            }
            return View();
        }
    }
}