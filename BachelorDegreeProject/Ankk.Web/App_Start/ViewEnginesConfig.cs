namespace Ankk.Web.App_Start
{
    using System.Web.Mvc;

    public static class ViewEnginesConfig
    {
        public static void RegisterViewEngines(ViewEngineCollection viewEngineCollection)
        {
            viewEngineCollection.Clear();
            viewEngineCollection.Add(new RazorViewEngine());
        }
    }
}