using System;
using System.Net.Http;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;
using VoteSystem.Clients.MVC.Controllers;
using VoteSystem.Clients.MVC.ViewModels.Account;
using VoteSystem.Data.Ef.Factories;
using VoteSystem.Data.Ef.Models;
using VoteSystem.Data.Entities;
using VoteSystem.Data.Entities.Factories;
using VoteSystem.Services.Identity.Contracts;

namespace VoteSystem.Clients.MVC.Tests.Controllers
{
    [TestFixture]
    public class AccountControllerTests
    {
        private Mock<IIdentityUserManagerService> _mockedIdentityUserManagerService;
        private Mock<IIdentitySignInService> _mockedIdentitySignInService;
        private Mock<IAspNetUserFactory> _mockedAspNetUserFactory;
        private Mock<IVoteSystemUserFactory> _mockedVoteSystemUserFactory;

        [SetUp]
        public void SetUp()
        {
            _mockedIdentityUserManagerService = new Mock<IIdentityUserManagerService>();
            _mockedIdentitySignInService = new Mock<IIdentitySignInService>();
            _mockedAspNetUserFactory = new Mock<IAspNetUserFactory>();
            _mockedVoteSystemUserFactory = new Mock<IVoteSystemUserFactory>();
        }

        [Test]
        public void Constructor_WithNullIdendityUserNamagerService_ShouldThrowsArgumentNullException()
        {
            var ex = Assert.Catch<ArgumentNullException>(() => 
                                                            new AccountController(
                                                                null, 
                                                                _mockedIdentityUserManagerService.Object,
                                                                _mockedAspNetUserFactory.Object,
                                                                _mockedVoteSystemUserFactory.Object));

            StringAssert.Contains("identitySignInService", ex.Message);
        }

        [Test]
        public void Constructor_WithNullIdentitySignInServic_ShouldThrowsArgumentNullException()
        {
            var ex = Assert.Catch<ArgumentNullException>(() =>
                new AccountController(
                    _mockedIdentitySignInService.Object,
                    null,
                    _mockedAspNetUserFactory.Object,
                    _mockedVoteSystemUserFactory.Object));

            StringAssert.Contains("identityUserManagerService", ex.Message);
        }

        [Test]
        public void Constructor_WithNullAspNetUserFactor_ShouldThrowsArgumentNullException()
        {
            var ex = Assert.Catch<ArgumentNullException>(() =>
                new AccountController(
                    _mockedIdentitySignInService.Object,
                    _mockedIdentityUserManagerService.Object,
                    null,
                    _mockedVoteSystemUserFactory.Object));

            StringAssert.Contains("aspNetUserFactory", ex.Message);
        }

        [Test]
        public void Constructor_WithNullVoteSystemUserFactory_ShouldThrowsArgumentNullException()
        {
            var ex = Assert.Catch<ArgumentNullException>(() =>
                new AccountController(
                    _mockedIdentitySignInService.Object,
                    _mockedIdentityUserManagerService.Object,
                    _mockedAspNetUserFactory.Object,
                    null));

            StringAssert.Contains("voteSystemUserFactory", ex.Message);
        }

        [Test]
        public void LoginOnGetRequest_WithAuthenticateUser_ShouldRedirectToDashboardIndex()
        {
            // Arrange
            var mockContext = new Mock<ControllerContext>();
            mockContext.Setup(p => p.HttpContext.User.Identity.IsAuthenticated).Returns(true);

            var accountController = new AccountController(
                _mockedIdentitySignInService.Object,
                _mockedIdentityUserManagerService.Object,
                _mockedAspNetUserFactory.Object,
                _mockedVoteSystemUserFactory.Object)
            {
                ControllerContext = mockContext.Object
            };
           
            string returnUrl = "url";
           

            // Act & Assert
            accountController
                .WithCallTo(c => c.Login(returnUrl))
                .ShouldRedirectTo<DashboardController>(x => x.Index());
        }

        [Test]
        public void LoginOnGetRequest_WithNotAuthenticateUser_ShouldRenderDefaultView()
        {
            // Arrange
            var mockContext = new Mock<ControllerContext>();
            mockContext.Setup(p => p.HttpContext.User.Identity.IsAuthenticated).Returns(false);

            var accountController = new AccountController(
                _mockedIdentitySignInService.Object,
                _mockedIdentityUserManagerService.Object,
                _mockedAspNetUserFactory.Object,
                _mockedVoteSystemUserFactory.Object)
            {
                ControllerContext = mockContext.Object
            };

            string returnUrl = "url";

            // Act & Assert
            accountController
                .WithCallTo(c => c.Login(returnUrl))
                .ShouldRenderDefaultView();
        }

        [Test]
        public void LoginOnPostRequest_WithInvalidModelState_ShouldRenderDefaultView()
        {
            // Arrange
            var accountController = new AccountController(
                _mockedIdentitySignInService.Object,
                _mockedIdentityUserManagerService.Object,
                _mockedAspNetUserFactory.Object,
                _mockedVoteSystemUserFactory.Object);

            accountController.ModelState.AddModelError("errorKey", "error");

            LoginViewModel model = new LoginViewModel();
            string returnUrl = "url";

            // Act & Assert
            accountController
                .WithCallTo(c => c.Login(model, returnUrl))
                .ShouldRenderDefaultView()
                .WithModel(model)
                .AndModelError("errorKey");
        }

        [Test]
        public async Task LoginOnPostRequest_WithValidModelStateAndSuccessResult_ShouldRedirectToLocalUrl()
        {
            // Arrange
            Mock<HttpContextBase> mockedHttpContextBase = new Mock<HttpContextBase>();
            UrlHelper _urlHelperMock = new UrlHelper(new RequestContext(mockedHttpContextBase.Object, new RouteData()));

            _mockedIdentitySignInService.Setup(
                 x => x.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(SignInStatus.Success);

            var accountController = new AccountController(
                _mockedIdentitySignInService.Object,
                _mockedIdentityUserManagerService.Object,
                _mockedAspNetUserFactory.Object,
                _mockedVoteSystemUserFactory.Object);
            accountController.Url = _urlHelperMock;

            LoginViewModel model = new LoginViewModel();
            string returnUrl = "/";

            // Act & Assert
            accountController
                .WithCallTo(c => c.Login(model, returnUrl))
                .ShouldRedirectTo(returnUrl);
        }

        [Test]
        public async Task LoginOnPostRequest_WithValidModelStateAndSuccessResult_ShouldRedirectToDashboardIndex()
        {
            // Arrange
            Mock<HttpContextBase> mockedHttpContextBase = new Mock<HttpContextBase>();
            UrlHelper _urlHelperMock = new UrlHelper(new RequestContext(mockedHttpContextBase.Object, new RouteData()));


            _mockedIdentitySignInService.Setup(
                    x => x.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(SignInStatus.Success);

            var accountController = new AccountController(
                _mockedIdentitySignInService.Object,
                _mockedIdentityUserManagerService.Object,
                _mockedAspNetUserFactory.Object,
                _mockedVoteSystemUserFactory.Object);
            accountController.Url = _urlHelperMock;

            LoginViewModel model = new LoginViewModel();
            string returnUrl = "url";

            // Act & Assert
            accountController
                .WithCallTo(c => c.Login(model, returnUrl))
                .ShouldRedirectTo<DashboardController>(x => x.Index());
        }

        [Test]
        public async Task LoginOnPostRequest_WithValidModelStateAndNotSuccessResult_ShouldRenderDefaultView()
        {
            // Arrange

            _mockedIdentitySignInService.Setup(
                    x => x.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(SignInStatus.Failure);

            var accountController = new AccountController(
                _mockedIdentitySignInService.Object,
                _mockedIdentityUserManagerService.Object,
                _mockedAspNetUserFactory.Object,
                _mockedVoteSystemUserFactory.Object);

            LoginViewModel model = new LoginViewModel();
            string returnUrl = "url";

            // Act & Assert
            accountController
                .WithCallTo(c => c.Login(model, returnUrl))
                .ShouldRenderDefaultView()
                .WithModel(model)
                .AndModelError("");
        }

        [Test]
        public void RegisterOnGetRequest_WithDefaultFlow_ShouldRenderDefaultView()
        {
            // Arrange
            var accountController = new AccountController(
                _mockedIdentitySignInService.Object,
                _mockedIdentityUserManagerService.Object,
                _mockedAspNetUserFactory.Object,
                _mockedVoteSystemUserFactory.Object);

            // Act & Assert
            accountController
                .WithCallTo(c => c.Register())
                .ShouldRenderDefaultView();
        }

        [Test]
        public void RegisterOnPostRequest_WithInvalidModel_ShouldRenderDefaultView()
        {
            // Arrange
            var accountController = new AccountController(
                _mockedIdentitySignInService.Object,
                _mockedIdentityUserManagerService.Object,
                _mockedAspNetUserFactory.Object,
                _mockedVoteSystemUserFactory.Object);

            accountController.ModelState.AddModelError("errorKey", "error");

            RegisterViewModel model = new RegisterViewModel();

            // Act & Assert
            accountController
                .WithCallTo(c => c.Register(model))
                .ShouldRenderDefaultView()
                .WithModel(model)
                .AndModelError("errorKey");
        }

        [Test]
        public void RegisterOnPostRequest_WithValidModelAndSucceededCreateResult_ShouldRedirectToAccountController()
        {
            // Arrange
            _mockedAspNetUserFactory.Setup(x => x.CreateAuthUser()).Returns(new AspNetUser());

            _mockedVoteSystemUserFactory.Setup(x => x.CreateVoteSystemUser()).Returns(new VoteSystemUser());

            _mockedIdentityUserManagerService.Setup(x => x.CreateAsync(It.IsAny<AspNetUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _mockedIdentitySignInService.Setup(
                    x => x.SignInAsync(It.IsAny<AspNetUser>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(0));

            var accountController = new AccountController(
                _mockedIdentitySignInService.Object,
                _mockedIdentityUserManagerService.Object,
                _mockedAspNetUserFactory.Object,
                _mockedVoteSystemUserFactory.Object);

            RegisterViewModel model = new RegisterViewModel();

            // Act & Assert
            accountController
                .WithCallTo(c => c.Register(model))
                .ShouldRedirectTo<AccountController>(x => x.Login(It.IsAny<string>()));
        }

        [Test]
        public void RegisterOnPostRequest_WithValidModelAndFailedCreateResult_ShouldRenderDefaultView()
        {
            // Arrange
            _mockedAspNetUserFactory.Setup(x => x.CreateAuthUser()).Returns(new AspNetUser());

            _mockedVoteSystemUserFactory.Setup(x => x.CreateVoteSystemUser()).Returns(new VoteSystemUser());

            _mockedIdentityUserManagerService.Setup(x => x.CreateAsync(It.IsAny<AspNetUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed());

            _mockedIdentitySignInService.Setup(
                    x => x.SignInAsync(It.IsAny<AspNetUser>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(0));

            var accountController = new AccountController(
                _mockedIdentitySignInService.Object,
                _mockedIdentityUserManagerService.Object,
                _mockedAspNetUserFactory.Object,
                _mockedVoteSystemUserFactory.Object);

            RegisterViewModel model = new RegisterViewModel();

            // Act & Assert
            accountController
                .WithCallTo(c => c.Register(model))
                .ShouldRenderDefaultView()
                .WithModel(model);
        }
    }
}
