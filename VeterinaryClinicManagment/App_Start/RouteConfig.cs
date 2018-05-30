﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace VeterinaryClinicManagment
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Login",
                url: "Session/{action}",
                defaults: new {controller="Session", action="SignIn"}
            );

            routes.MapRoute(
                name: "Def",
                url: "{controller}/{id}/{action}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                constraints: new {id= @"\d+" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index"}
            );
        }
    }
}
