using System.Web.Mvc;
using System.Web.Routing;

namespace TestSite
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new {controller = "Forum", action = "Index", id = UrlParameter.Optional}
                );
        }
    }
}