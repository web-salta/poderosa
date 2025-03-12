using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace proyecto_poderosa_documento.App_Start
{
    public class FriendlyRoute : Route
    {
        public FriendlyRoute(string url, object defaults) :
           base(url, new RouteValueDictionary(defaults), new MvcRouteHandler())
        { }
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            var routeData = base.GetRouteData(httpContext);
            RouteValueDictionary values = routeData.Values;
            values["controller"] = (values["controller"] as string).Replace("-", "_");
            values["action"] = (values["action"] as string).Replace("-", "_");
            return routeData;
        }

        public override VirtualPathData GetVirtualPath(RequestContext ctx, RouteValueDictionary values)
        {
            values["controller"] = (values["controller"] as string).Replace("_", "-").ToLower();
            values["action"] = (values["action"] as string).Replace("_", "-").ToLower();
            return base.GetVirtualPath(ctx, values);
        }
    }
}