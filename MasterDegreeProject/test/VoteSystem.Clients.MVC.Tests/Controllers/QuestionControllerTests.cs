using System;
using Moq;
using NUnit.Framework;
using VoteSystem.Clients.MVC.Areas.Administration.Controllers;
using VoteSystem.Data.Services.Contracts;

namespace VoteSystem.Clients.MVC.Tests.Controllers
{
    [TestFixture]
    public class QuestionControllerTests
    {
        private Mock<IQuestionService> _mockedQuestionService;

        [SetUp]
        public void Setup()
        {
            _mockedQuestionService = new Mock<IQuestionService>();
            AutomapperConfig.RegisterMap();
        }

        [Test]
        public void Constructor_WithNullQuestionService_ShouldThrowArgumenNullException()
        {
            var ex = Assert.Catch<ArgumentNullException>(() => new QuestionController(null));

            StringAssert.Contains("questionService", ex.Message);
        }

        [Test]
        public void Constructor_WithValidParam_ShouldReturnsInstanceOfQuestionController()
        {
            var actualInstance = new QuestionController(_mockedQuestionService.Object);

            Assert.That(actualInstance, Is.Not.Null);
            Assert.That(actualInstance, Is.InstanceOf<QuestionController>());
        }

        //[TestCase(0)]
        //[TestCase(-1)]
        //[TestCase(-100)]
        //public void CreateOnGetRequest_WithInvalidId_ShouldReturnsMinusOne(int voteSystemId)
        //{
        //    var controller = new QuestionController(_mockedQuestionService.Object);

        //    var actionResult = controller.Create(voteSystemId) as ContentResult;

        //    Assert.AreEqual("voteSystemId can not be negative number or 0", actionResult.Content);
        //}

        //[TestCase(1)]
        //[TestCase(100)]
        //public void CreateOnGetRequest_WithValidId_ShouldReturnsQuestionAndAnswersViewModel(Guid voteSystemId)
        //{
        //    var controller = new QuestionController(_mockedQuestionService.Object);

        //    var actionResult = controller.Create(voteSystemId) as ViewResult;

        //    Assert.IsNotNull(actionResult);
        //}

        //[Test]
        //public void CreateOnPostRequest_WithInvalidModelState_ShouldReturnCoreectViewModel()
        //{
        //    var expectedRateSystemId = 17;
        //    var expectedQuestionId = 15;
        //    var expectedQuestionName = "Ankk";

        //    var vm = new List<QuestionViewModel>();

        //    var controller = new QuestionController(_mockedQuestionService.Object);
        //    controller.ModelState.AddModelError("error", "error");

        //    var actionResult = controller.Create(vm) as ViewResult;
        //    var actualResult = actionResult.Model as QuestionAndAnswersViewModel;

        //    Assert.IsNotNull(actualResult);
        //    Assert.AreEqual(actualResult.VoteSystemId, expectedRateSystemId);
        //    Assert.AreEqual(actualResult.Questions.Count(), 1);
        //    Assert.AreEqual(actualResult.Questions[0].Id, expectedQuestionId);
        //    Assert.AreEqual(actualResult.Questions[0].QuestionName, expectedQuestionName);
        //}

        //[Test]
        //public void CreateOnPostRequest_WithNullViewModel_ShouldReturnNull()
        //{
        //    var controller = new QuestionController(_mockedQuestionService.Object);
        //    controller.ModelState.AddModelError("error", "error");

        //    var actionResult = controller.Create(null) as ViewResult;
        //    var actualResult = actionResult.Model as QuestionAndAnswersViewModel;

        //    Assert.IsNull(actualResult);
        //}

        //[Test]
        //public void CreateOnPostRequest_WithZeroQuestions_ShouldReturnCoreectViewModel()
        //{
        //    var expectedRateSystemId = 17;

        //    var vm = new QuestionAndAnswersViewModel()
        //    {
        //        VoteSystemId = expectedRateSystemId,
        //        Questions = new List<QuestionViewModel>()
        //        {
        //        }
        //    };

        //    var controller = new QuestionController(_mockedQuestionService.Object);

        //    var actionResult = controller.Create(vm) as ViewResult;
        //    var actualResult = actionResult.Model as QuestionAndAnswersViewModel;

        //    Assert.IsNotNull(actualResult);
        //    Assert.AreEqual(actualResult.VoteSystemId, expectedRateSystemId);
        //}

        //[Test]
        //public void CreateOnPostRequest_WithValidViewModel_ShouldCallSave()
        //{
        //    var expectedRateSystemId = 17;
        //    var expectedQuestionName = "Ankk";

        //    var vm = new QuestionAndAnswersViewModel()
        //    {
        //        VoteSystemId = expectedRateSystemId,
        //        Questions = new List<QuestionViewModel>()
        //        {
        //            new QuestionViewModel()
        //            {
        //                QuestionName = expectedQuestionName
        //            }
        //        }
        //    };

        //    var controller = new QuestionController(_mockedQuestionService.Object);
        //    controller.Create(vm);

        //    //_mockedQuestionService.Verify(
        //    //    x => x.Add(It.Is<AddNewQuestion>(
        //    //        q => q.QuestionName == expectedQuestionName)), 
        //    //        Times.Once);
        //}

        //[Test]
        //public void CreateOnPostRequest_WithDefaultFlow_ShouldRedirectToIndex()
        //{
        //    var expectedRateSystemId = 17;
        //    var expectedQuestionName = "Ankk";

        //    var vm = new QuestionAndAnswersViewModel()
        //    {
        //        VoteSystemId = expectedRateSystemId,
        //        Questions = new List<QuestionViewModel>()
        //        {
        //            new QuestionViewModel()
        //            {
        //                QuestionName = expectedQuestionName
        //            }
        //        }
        //    };

        //    var controller = new QuestionController(_mockedQuestionService.Object);
        //    var redirectResult = controller.Create(vm) as RedirectToRouteResult;

        //    Assert.AreEqual("Index", redirectResult.RouteValues["Action"]);
        //}
    }
}
