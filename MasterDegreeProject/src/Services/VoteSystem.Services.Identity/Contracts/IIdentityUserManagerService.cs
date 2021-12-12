using System;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;

using VoteSystem.Data.Ef.Models;

namespace VoteSystem.Services.Identity.Contracts
{
    public interface IIdentityUserManagerService : IDisposable
    {
        Task<IdentityResult> CreateAsync(AspNetUser user, string password);

        Task<IdentityResult> ConfirmEmailAsync(string userId, string token);

        Task<bool> IsEmailConfirmedAsync(string userId);

        Task<AspNetUser> FindByNameAsync(string userName);

        Task<IdentityResult> ResetPasswordAsync(string userId, string token, string newPassword);

        Task<AspNetUser> FindByIdAsync(string userId);

        Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword);

        Task<string> GeneratePasswordResetTokenAsync(string userId);

        Task<IdentityResult> UpdateAsync(AspNetUser user);

        Task SendEmailAsync(string userId, string subject, string body);

        Task<string> GenerateEmailConfirmationTokenAsync(string userId);
    }
}
