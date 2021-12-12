using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ankk.Web.Startup))]
namespace Ankk.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
