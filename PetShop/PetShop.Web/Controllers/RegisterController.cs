using AutoMapper;
using PetShop.BusinessLogic;
using PetShop.BusinessLogic.Interfaces;
using PetShop.Domain.Entities.Response;
using PetShop.Domain.Entities.User;
using PetShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetShop.Web.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ISesion _session;
        public RegisterController()
        {
            var bl = new BussinessLogic();
            _session = bl.GetSessionBL();
        }
        // GET: Register
        public ActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UserRegister register)
        {
            if (ModelState.IsValid)
            {
                var data = Mapper.Map<URegisterData>(register);

                data.LoginIp = Request.UserHostAddress;
                data.LoginDateTime = DateTime.Now;

                var guestCookie = Request.Cookies["PetShop-Guest"];
                var response = new Response();
                if(guestCookie != null) 
                {
                    var guestProfile = _session.GetUserByCookie(guestCookie.Value);
                    response = _session.UserRegister(data, guestProfile);
                    System.Web.HttpContext.Current.Session.Clear();
                    if (ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("PetShop-Guest"))
                    {
                        var cookie = ControllerContext.HttpContext.Request.Cookies["PetShop-Guest"];
                        if (cookie != null)
                        {
                            cookie.Expires = DateTime.Now.AddDays(-1);
                            ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                        }
                    }
                }
                else response = _session.UserRegister(data, null);
               
                if (response.Status)
                {
                    HttpCookie cookie = _session.GenCookie(register.Email);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);

                    return RedirectToAction("Index", "Home");
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