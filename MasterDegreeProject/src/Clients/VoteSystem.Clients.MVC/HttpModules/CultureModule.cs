using System;
using System.Linq;
using System.Threading;
using System.Web;

namespace VoteSystem.Clients.MVC.HttpModules
{
    // add CultureModule in Web.config
    // <add name="CultureModule" type="VoteSystem.Clients.MVC.HttpModules.CultureModule" />
    public class CultureModule : IHttpModule
    {
        private const string EN = "en";

        public void Init(HttpApplication context)
        {
            context.BeginRequest += this.Context_BeginRequest;
        }

        public void Dispose()
        {
        }

        private void Context_BeginRequest(object sender, EventArgs e)
        {
            var urlParts = HttpContext.Current.Request.Url.AbsoluteUri.Split(new [] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            if (urlParts.Count() > 2)
            {
                string lang = urlParts[2];

                if (lang == EN)
                {
                    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(EN);
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(EN);
                }
            }
        }
    }
}