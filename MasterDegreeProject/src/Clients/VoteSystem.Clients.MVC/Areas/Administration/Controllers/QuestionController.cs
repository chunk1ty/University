using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Expressions;

using VoteSystem.Clients.MVC.Areas.Administration.ViewModels.Question;
using VoteSystem.Clients.MVC.Infrastructure.Attributes;
using VoteSystem.Clients.MVC.Infrastructure.Mapping;
using VoteSystem.Clients.MVC.Infrastructure.NotificationSystem;
using VoteSystem.Common.Constants;
using VoteSystem.Data.Entities;
using VoteSystem.Data.Services.Contracts;

namespace VoteSystem.Clients.MVC.Areas.Administration.Controllers
{
    public class QuestionController : AdminController
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService ?? throw new ArgumentNullException(nameof(questionService));
        }

        [HttpGet]
        public ActionResult Create(Guid voteSystemId)
        {
            ViewBag.VoteSystemId = voteSystemId;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IList<QuestionViewModel> questions)
        {
            if (!ValidatePostRequest(questions))
            {
                return View(questions);
            }

            try
            {
                var questionsAsDbEntities = questions.To<Question>().ToList();
                _questionService.AddRange(questionsAsDbEntities);

                this.AddNotification("Успешно добавихте въпроси!", NotificationType.Success);
            }
            catch (Exception)
            {
                //TODO add login logic
                this.AddNotification("Възникна грешка при добавянето на въпросите!", NotificationType.Error);
            }

            return this.RedirectToAction<VoteSystemController>(c => c.Index());
        }

        [HttpGet]
        public ActionResult Edit(Guid voteSystemId)
        {
            var questionsAsViewModel = _questionService
                                    .GetQuestionsWithAnswersByVoteSystemId(voteSystemId)
                                    .To<QuestionViewModel>()
                                    .ToList();

            ViewBag.VoteSystemId = voteSystemId;

            return View(questionsAsViewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IList<QuestionViewModel> questions)
        {
            if (!ValidatePostRequest(questions))
            {
                return View(questions);
            }

            try
            {
                var questionsAsDbEntities = questions.To<Question>().ToList();
                _questionService.UpdateRange(questionsAsDbEntities);
               
                this.AddNotification("Успешно редактирахте въпросите!", NotificationType.Success);
            }
            catch (Exception)
            {
                //TODO add login logic
                this.AddNotification("Възникна грешка при редактирането на въпросите!", NotificationType.Error);
            }

            return this.RedirectToAction<VoteSystemController>(c => c.Index());
        }

        [HttpGet]
        public ActionResult Preview(Guid voteSystemId)
        {
            var questionsAsViewModel = _questionService
                                                    .GetQuestionsWithAnswersByVoteSystemId(voteSystemId)
                                                    .To<QuestionViewModel>()
                                                    .ToList();

            return View(questionsAsViewModel);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult AddNewQuestion(Guid voteSystemId)
        {
            var questionViewModel = new QuestionViewModel
            {
                VoteSystemId = voteSystemId
            };

            return PartialView(PartialViewConstants.QuestionPartial, questionViewModel);
        }

        private bool ValidatePostRequest(IEnumerable<QuestionViewModel> model)
        {
            bool isValid = true;

            if (!ModelState.IsValid || model == null)
            {
                isValid = false;
                return isValid;
            }

            if (!model.Any())
            {
                ModelState.AddModelError(string.Empty, "Моля добавете най-малко един въпрос!");
                isValid = false;
            }

            return isValid;
        }
    }
}