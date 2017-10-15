using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Simple02
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //can not use IgnoreRoute to control page access !

            /*
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{role}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, role = UrlParameter.Optional }
            );

            
            routes.MapRoute(
                name: "Search",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Search", id = UrlParameter.Optional }
            );*/

            routes.MapRoute(
                name: "Home",
                url: "{controller}/{action}/{role}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, role = UrlParameter.Optional }
            );

            /*
            routes.MapRoute(
                            name: "ApplyRole",
                            url: "{controller}/{action}/{id}",
                            defaults: new { controller = "Account", action = "ApplyRole", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Submit",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Submit", id = UrlParameter.Optional }
            );*/




        }
    }
}
