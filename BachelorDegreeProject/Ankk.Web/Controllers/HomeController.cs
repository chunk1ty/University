namespace Ankk.Web.Controllers
{
    using System.IO;
    using System.Web.Mvc;
    
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.IsInRole("Administrator"))
            {
                Directory.CreateDirectory(Server.MapPath("~/App_Data/Admin"));
                return RedirectToAction("Index", "Administration");
            }
            else
            {
                return RedirectToAction("Index", "Student");
            }

            //return this.View();
        }
    }
}