using System.Web.Mvc;
using System.Web.Routing;

namespace proyecto_poderosa_documento
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Rutas específicas para evitar conflictos con la redirección de slug
            routes.MapRoute(
                name: "NoticiasIndex",
                url: "Noticias/Index",
                defaults: new { controller = "Noticias", action = "Index" }
            );
            routes.MapRoute(
                name: "NoticiasRoot",
                url: "Noticias",
                defaults: new { controller = "Noticias", action = "Index" }
            );


            routes.MapMvcAttributeRoutes(); // Habilita rutas por atributo

            // Ruta personalizada para Noticias/Details/{slug}
            routes.MapRoute(
                name: "NoticiasDetails",
                url: "Noticias/Details/{slug}",
                defaults: new { controller = "Noticias", action = "Details", slug = UrlParameter.Optional }
            );

            // Agregar una ruta específica para la acción Create
            routes.MapRoute(
                name: "NoticiasCreate",
                url: "noticias/create",
                defaults: new { controller = "Noticias", action = "Create" }
            );

            // Ruta predeterminada
            routes.Add("default",
              new App_Start.FriendlyRoute(
                  "{controller}/{action}/{accion}/{id}", // URL con parámetros
                  new { controller = "Home", action = "Index", accion = "", id = UrlParameter.Optional }
              )
            );
        }
    }
}
