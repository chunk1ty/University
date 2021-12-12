using System;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity.Owin;

using VoteSystem.Data.Ef.Models;

namespace VoteSystem.Services.Identity.Contracts
{
    public interface IIdentitySignInService : IDisposable
    {
        Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout);

        Task SignInAsync(AspNetUser user, bool isPersistent, bool rememberBrowser);
    }
}
