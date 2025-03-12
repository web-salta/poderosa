using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace proyecto_poderosa_documento
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            /*routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );*/
            routes.Add("default",
              new App_Start.FriendlyRoute(
                  "{controller}/{action}/{accion}/{id}", // URL con parámetros
                  new { controller = "Home", action = "Index", accion = "", id = UrlParameter.Optional }
              )
            );
                 /* routes.MapRoute(
                  name: "Default",
                  url: "{controller}/{*action}",
                  defaults: new { controller = "Home", action = "Index" }
              );*/

        }
    }
}
