using System.Web.Mvc;

namespace VoteSystem.Clients.MVC.Areas.Administration
{
    public class AdministrationAreaRegistration : AreaRegistration 
    {
        public override string AreaName => "Administration";

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                name: "Administration_default",
                url: "admin/{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "VoteSystem",
                    action = "Index",
                    id = UrlParameter.Optional
                });
        }
    }
}