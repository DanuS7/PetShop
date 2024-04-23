using AutoMapper;
using PetShop.BusinessLogic;
using PetShop.BusinessLogic.Interfaces;
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

                var userRegister = _session.UserRegister(data);
                if (userRegister.Status)
                {
                    HttpCookie cookie = _session.GenCookie(register.Email);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", userRegister.ActionStatusMsg);
                    return View();
                }
            }
            return View();
        }
    }
}