using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Expressions;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

using VoteSystem.Clients.MVC.Infrastructure.NotificationSystem;
using VoteSystem.Clients.MVC.ViewModels.Account;
using VoteSystem.Data.Ef.Factories;
using VoteSystem.Data.Ef.Models;
using VoteSystem.Data.Entities.Factories;
using VoteSystem.Services.Identity.Contracts;

namespace VoteSystem.Clients.MVC.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private IIdentityUserManagerService _identityUserManagerService;
        private readonly IIdentitySignInService _identitySignInService;
        private readonly IAspNetUserFactory _aspNetUserFactor;
        private readonly IVoteSystemUserFactory _voteSystemUserFactory;

        public AccountController(
            IIdentitySignInService identitySignInService, 
            IIdentityUserManagerService identityUserManagerService, 
            IAspNetUserFactory aspNetUserFactory,
            IVoteSystemUserFactory voteSystemUserFactory)
        {
            _identitySignInService = identitySignInService ?? throw new ArgumentNullException(nameof(identitySignInService));
            _identityUserManagerService = identityUserManagerService ?? throw new ArgumentNullException(nameof(identityUserManagerService));
            _aspNetUserFactor = aspNetUserFactory ?? throw new ArgumentNullException(nameof(aspNetUserFactory));
            _voteSystemUserFactory = voteSystemUserFactory ?? throw new ArgumentNullException(nameof(voteSystemUserFactory));
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction<DashboardController>(x => x.Index());
            }

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await _identitySignInService.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

            if (result == SignInStatus.Success)
            {
                return RedirectToLocal(returnUrl);
            }
          
            ModelState.AddModelError(string.Empty, "Невалиден имейл или парола.");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return this.RedirectToAction<HomeController>(x => x.Index());
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _aspNetUserFactor.CreateAuthUser();

                user.UserName = model.Email;
                user.Email = model.Email;
                user.FacultyNumber = model.FacultyNumber;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;

                user.VoteSystemUser = _voteSystemUserFactory.CreateVoteSystemUser();
                user.VoteSystemUser.Email = model.Email;
                user.VoteSystemUser.FacultyNumber = model.FacultyNumber;
                user.VoteSystemUser.FirstName = model.FirstName;
                user.VoteSystemUser.LastName = model.LastName;
                user.VoteSystemUser.Id = new Guid(user.Id);

                var result = await _identityUserManagerService.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // generate token
                    //await SendEmalForNewUser(user);
                    //AddNotification("Проверете имейлът си за да активирате акаунта.", NotificationType.Warning);

                    await _identitySignInService.SignInAsync(user, false, false);

                    return this.RedirectToAction<AccountController>(c => c.Login(string.Empty));
                }

                AddErrors(result);
            }
            
            return View(model);
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _identityUserManagerService.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);

            if (result.Succeeded)
            {
                var user = await _identityUserManagerService.FindByIdAsync(User.Identity.GetUserId());

                if (user != null)
                {
                    await _identitySignInService.SignInAsync(user, false, false);
                }

                return this.RedirectToAction<DashboardController>(x => x.Index());
            }

            AddErrors(result);

            return this.View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ForgotPassword(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _identityUserManagerService.FindByNameAsync(model.Email);

                if (user == null || !(await _identityUserManagerService.IsEmailConfirmedAsync(user.Id)))
                {
                    this.AddNotification("Въведеният имейл не съществува.", NotificationType.Error);

                    return this.RedirectToAction<AccountController>(c => c.Login(string.Empty));
                }

                //await SendEmailForForgottenPasswordAsync(user);

                this.AddNotification("Проверете вашият имейл за въвеждане на нова парола.", NotificationType.Info);

                return this.RedirectToAction<AccountController>(c => c.Login(string.Empty));
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _identityUserManagerService.FindByNameAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError("IncorectEmail", "Имейл адресът не съществува.");

                return View(model);
            }

            var result = await _identityUserManagerService.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                this.AddNotification("Успешно променихте вашата парола.", NotificationType.Success);

                return this.RedirectToAction<AccountController>(c => c.Login(string.Empty));
            }

            ModelState.AddModelError(string.Empty, "Невалиден имейл адрес или токен.");

            return View(model);

        }

        [HttpGet]
        public async Task<ActionResult> UserProfile()
        {
            string userId = User.Identity.GetUserId();

            var user = await _identityUserManagerService.FindByIdAsync(userId);

            UserInfoViewModel userVm = new UserInfoViewModel()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                FacultyNumber = user.FacultyNumber
            };           

            return View(userVm);
        }

        [HttpGet]
        public async Task<ActionResult> EditUserProfile()
        {
            string userId = User.Identity.GetUserId();

            var user = await _identityUserManagerService.FindByIdAsync(userId);

            UserInfoViewModel userVM = new UserInfoViewModel()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                FacultyNumber = user.FacultyNumber
            };

            return View(userVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUserProfile(UserInfoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string userId = User.Identity.GetUserId();

            var user = await _identityUserManagerService.FindByIdAsync(userId);
            
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.FacultyNumber = model.FacultyNumber;

            await _identityUserManagerService.UpdateAsync(user);

            this.AddNotification("Успешно променихте вашите данни.", NotificationType.Success);

            return this.RedirectToAction<AccountController>(x => x.UserProfile());
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }

            var result = await _identityUserManagerService.ConfirmEmailAsync(userId, code);

            if (result.Succeeded)
            {
                this.AddNotification("Успешно активирахте вашият акаунт.", NotificationType.Success);
            }
            else
            {
                this.AddNotification("Неуспешно активирахте вашият акаунт.", NotificationType.Error);
            }

            return this.RedirectToAction<AccountController>(c => c.Login(string.Empty));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_identityUserManagerService != null)
                {
                    _identityUserManagerService.Dispose();
                    _identityUserManagerService = null;
                }

                if (_identityUserManagerService != null)
                {
                    _identityUserManagerService.Dispose();
                    _identityUserManagerService = null;
                }
            }

            base.Dispose(disposing);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return this.RedirectToAction<DashboardController>(x => x.Index());
        }

        // TODO move in email service
        private async Task SendEmailForForgottenPasswordAsync(AspNetUser aspNetUser)
        {
            string code = await _identityUserManagerService.GeneratePasswordResetTokenAsync(aspNetUser.Id);
            var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = aspNetUser.Id, code = code }, protocol: Request.Url.Scheme);

            UriBuilder url = new UriBuilder(callbackUrl);
            url.Port = -1;
            callbackUrl = url.Uri.ToString();

            await _identityUserManagerService.SendEmailAsync(aspNetUser.Id, "Забравена парола.", "Въведете новата парола като натиснете: <a href=\"" + callbackUrl + "\">тук.</a>");
        }

        // TODO move in email service
        private async Task SendEmalForNewUser(AspNetUser aspNetUser)
        {
            await _identitySignInService.SignInAsync(aspNetUser, isPersistent: false, rememberBrowser: false);

            string code = await _identityUserManagerService.GenerateEmailConfirmationTokenAsync(aspNetUser.Id);
            var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = aspNetUser.Id, code = code }, protocol: Request.Url.Scheme);

            UriBuilder url = new UriBuilder(callbackUrl);
            url.Port = -1;
            callbackUrl = url.Uri.ToString();

            await _identityUserManagerService.SendEmailAsync(aspNetUser.Id, "Потвърждаване на имейл.", "Моля потвърдете вашият имейл като натиснете <a href=\"" + callbackUrl + "\">тук.</a>");
        }
    }
}