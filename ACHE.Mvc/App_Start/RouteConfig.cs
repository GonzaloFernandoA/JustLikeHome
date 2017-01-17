using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ACHE.Mvc {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.aspx/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Listado con parámetros",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Listado", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Detalle",
                url: "{controller}/{action}/{name}/{id}",
                defaults: new { controller = "Home", action = "Detalle", id = UrlParameter.Optional, name = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Reserva",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Reserva", id = UrlParameter.Optional }
            );
        }
    }
}