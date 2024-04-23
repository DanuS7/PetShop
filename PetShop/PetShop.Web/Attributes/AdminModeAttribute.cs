﻿using AutoMapper;
using PetShop.BusinessLogic;
using PetShop.BusinessLogic.Interfaces;
using PetShop.Domain.Entities.User;
using PetShop.Domain.Enums;
using PetShop.Web.Extensions;
using PetShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PetShop.Web.Attributes
{
    public class AdminModeAttribute : ActionFilterAttribute
    {
        private readonly ISesion _sessionBusinessLogic;
        public AdminModeAttribute()
        {
            var businessLogic = new BussinessLogic();
            _sessionBusinessLogic = businessLogic.GetSessionBL();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var apiCookie = HttpContext.Current.Request.Cookies["X-KEY"];
            if (apiCookie != null)
            {
                var profile = _sessionBusinessLogic.GetUserByCookie(apiCookie.Value);
                if (profile != null && profile.Level == URole.admin)
                {
                    HttpContext.Current.SetMySessionObject(profile);
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Index" }));
                }
            }
        }
    }
}