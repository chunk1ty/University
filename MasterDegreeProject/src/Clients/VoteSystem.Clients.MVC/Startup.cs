using Microsoft.Owin;
using Owin;
using VoteSystem.Clients.MVC;

[assembly: OwinStartup(typeof(Startup))]
namespace VoteSystem.Clients.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}
