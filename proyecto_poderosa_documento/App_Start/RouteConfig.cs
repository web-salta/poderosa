using System.Web.Mvc;
using System.Web.Routing;

namespace proyecto_poderosa_documento
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Modificar la ruta de Noticias para aceptar un parámetro de ruta llamado "titulo"
            routes.MapRoute(
                name: "NoticiasDetalles",
                url: "noticias/details/{titulo}",
                defaults: new { controller = "Noticias", action = "Details" }
            );

            routes.Add("default",
              new App_Start.FriendlyRoute(
                  "{controller}/{action}/{accion}/{id}", // URL con parámetros
                  new { controller = "Home", action = "Index", accion = "", id = UrlParameter.Optional }
              )
            );

        }
    }
}
