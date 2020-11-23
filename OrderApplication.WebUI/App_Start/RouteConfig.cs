using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OrderApplication.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: null,
                url: "Page{page}",
                defaults: new { controller = "Product", action = "ProducList" }
                );

            routes.MapRoute(null,
            "{category}",
             new { controller = "Product", action = "ProducList", page = 1 }
             );

            routes.MapRoute(null,
                 "{category}/Page{page}",
                 new { controller = "Product", action = "ProducList" },
                 new { page = @"\d+" }
                 );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Product", action = "ProducList", id = UrlParameter.Optional }
            );
           
        }
    }
}
