using System.Security.Principal;

using VoteSystem.Common.Constants;

namespace VoteSystem.Clients.MVC.Infrastructure.Extensions
{
    public static class PrincipalExtensions
    {
        public static bool IsAdministrator(this IPrincipal principal)
        {
            if (principal == null)
            {
                return false;
            }

            return principal.IsInRole(GlobalConstants.AdministratorRoleName);
        }
    }
}
