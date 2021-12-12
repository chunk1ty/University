using Glimpse.AspNet.Extensions;
using Glimpse.Core.Extensibility;

using VoteSystem.Clients.MVC.Infrastructure.Extensions;

namespace VoteSystem.Clients.MVC
{
    public class GlimpseSecurityPolicy : IRuntimePolicy
    {
        public RuntimeEvent ExecuteOn
        {
            //The RuntimeEvent.ExecuteResource is only needed in case you create a security policy
            // Have a look at http://blog.getglimpse.com/2013/12/09/protect-glimpse-axd-with-your-custom-runtime-policy/ for more details
            get
            {
                return RuntimeEvent.EndRequest | RuntimeEvent.ExecuteResource;
            }
        }

        public RuntimePolicy Execute(IRuntimePolicyContext policyContext)
        {
            //You can perform a check like the one below to control Glimpse's permissions within your application.
            // More information about RuntimePolicies can be found at http://getglimpse.com/Help/Custom-Runtime-Policy
            var httpContext = policyContext.GetHttpContext();
           
            if (!httpContext.User.IsAdministrator())
            {
                return RuntimePolicy.Off;
            }

            return RuntimePolicy.On;
        }
    }
}
