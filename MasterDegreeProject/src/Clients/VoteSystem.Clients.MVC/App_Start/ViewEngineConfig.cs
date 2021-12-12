using System.Web.Mvc;

namespace VoteSystem.Clients.MVC
{
    public class ViewEngineConfig
    {
        public static void RegisterViewEngine()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
        }
    }
}