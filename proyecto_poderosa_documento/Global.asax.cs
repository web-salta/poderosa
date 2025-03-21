using System;
using System.Web;
using System.Web.Mvc;
namespace proyecto_poderosa_documento
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(System.Web.Routing.RouteTable.Routes);

            // Habilitar enrutamiento por atributos
            System.Web.Routing.RouteTable.Routes.MapMvcAttributeRoutes();
        }


        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            Response.Clear();

            HttpException httpException = exception as HttpException;

            int error = httpException != null ? httpException.GetHttpCode() : 0;

            Server.ClearError();
            // Sanitize the exception message to remove newline characters
            string sanitizedMessage = exception.Message.Replace("\r", "").Replace("\n", " ");
            Response.Redirect(String.Format("~/Error/?error={0}&mensaje={1}", error, sanitizedMessage));
        }
    }
}