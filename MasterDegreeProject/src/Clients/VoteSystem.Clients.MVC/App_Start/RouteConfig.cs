using System.Web.Mvc;
using System.Web.Routing;

namespace VoteSystem.Clients.MVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.LowercaseUrls = true;

            //routes.MapRoute(
            //    "SpecificRoute", 
            //    "{action}", 
            //    new { controller = "Introduction", action = "Index"});

            routes.MapRoute(
                name: "Intro",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" });

            //routes.MapRoute(
            //   name: "Default",
            //   url: "{controller}/{action}",
            //   defaults: new { controller = "Introduction", action = "Intro"});
        }
    }
}
