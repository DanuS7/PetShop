using AutoMapper;
using Microsoft.Win32;
using PetShop.BusinessLogic;
using PetShop.BusinessLogic.Interfaces;
using PetShop.Domain.Entities.User;
using PetShop.Domain.Enums;
using PetShop.Web.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetShop.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly ISesion _session;

        public BaseController()
        {
            var bl = new BussinessLogic();
            _session = bl.GetSessionBL();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            SessionStatus();
            base.OnActionExecuting(filterContext);
        }

        private string GenerateIdentifier()
        {
            Guid guid = Guid.NewGuid();
            byte[] bytes = guid.ToByteArray();
            long longValue = BitConverter.ToInt64(bytes, 0);
            return Math.Abs(longValue).ToString();
        }

        private void SessionStatus()
        {
            var apiCookie = Request.Cookies["X-KEY"];
            var guestCookie = Request.Cookies["PetShop-Guest"];
            bool loggedIn = false;
            if (apiCookie != null)
            {
                loggedIn = HandleAuthUsers(apiCookie);
            }
            if (loggedIn) return;

            if (guestCookie != null)
            {
                HandleGuestUsers(guestCookie);
            }
            else
            {
                TreatAsGuest();
            }
        }



        private bool HandleAuthUsers(HttpCookie apiCookie)
        {
            var profile = _session.GetUserByCookie(apiCookie.Value);
            if (profile != null)
            {
                System.Web.HttpContext.Current.SetMySessionObject(profile);
                switch (profile.Level)
                {
                    case URole.user:
                        System.Web.HttpContext.Current.Session["LoginStatus"] = "user";
                        break;
                    case URole.admin:
                        System.Web.HttpContext.Current.Session["LoginStatus"] = "admin";
                        break;
                    default: break;
                }
                return true;
            }
            else
            {
                System.Web.HttpContext.Current.Session.Clear();
                if (ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("X-KEY"))
                {
                    var cookie = ControllerContext.HttpContext.Request.Cookies["X-KEY"];
                    if (cookie != null)
                    {
                        cookie.Expires = DateTime.Now.AddDays(-1);
                        ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                    }
                }
                return false;
            }
        }

        private void HandleGuestUsers(HttpCookie guestCookie)
        {
            var profile = _session.GetUserByCookie(guestCookie.Value);
            if (profile != null)
            {
                System.Web.HttpContext.Current.SetMySessionObject(profile);
                System.Web.HttpContext.Current.Session["LoginStatus"] = "guest";
            }
            else
            {
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
                TreatAsGuest();
            }
        }

        private void TreatAsGuest()
        {

            var newGuest = new GuestRegisterData()
            {
                Username = "guest" + GenerateIdentifier(),
                LoginIp = Request.UserHostAddress,
                LoginDateTime = DateTime.Now,
            };

            var guestRegister = _session.GuestRegister(newGuest);
            if(guestRegister.Status)
            {
                HttpCookie cookie = _session.GenGuestCookie(newGuest.Username);
                ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                var profile = _session.GetUserByCookie(cookie.Value);
                System.Web.HttpContext.Current.SetMySessionObject(profile);
                System.Web.HttpContext.Current.Session["LoginStatus"] = "guest";
            }      
        }

       
    }
}