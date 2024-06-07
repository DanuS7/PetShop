using AutoMapper;
using PetShop.BusinessLogic;
using PetShop.BusinessLogic.Interfaces;
using PetShop.Domain.Entities.User;
using PetShop.Web.Extensions;
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

        private readonly ISesion _session;
        // GET: Login
        public LoginController()
        {
            var bl = new BussinessLogic();
            _session = bl.GetSessionBL();
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
                var data = Mapper.Map<ULoginData>(login);

                data.UserIp = Request.UserHostAddress;
                data.LoginDate = DateTime.Now;

                var userLogin = _session.UserLogin(data);
                if (userLogin.Status)
                {
                    HttpCookie cookie = _session.GenCookie(login.Email);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                    if(userLogin.ActionStatusMsg != null && userLogin.ActionStatusMsg == "admin")
                    {
                        return RedirectToAction("Dashboard", "Admin");
                    }
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

        public ActionResult Logout()
        {
            var currentUser = System.Web.HttpContext.Current.GetMySessionObject();
            var response = _session.UserLogout(currentUser);
            if(response.Status)
            {
                return RedirectToAction("Index", "Home");
            }
            else 
            {
                ModelState.AddModelError("", response.ActionStatusMsg);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}