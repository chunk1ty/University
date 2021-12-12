using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Expressions;

using Microsoft.AspNet.Identity;

using VoteSystem.Clients.MVC.Infrastructure.Mapping;
using VoteSystem.Clients.MVC.Infrastructure.NotificationSystem;
using VoteSystem.Clients.MVC.ViewModels.ParticipantFillOut;
using VoteSystem.Data.Services.Contracts;
using VotySystem.Data.DTO;

namespace VoteSystem.Clients.MVC.Controllers
{
    public class ParticipantFillOutController : BaseController
    {
        private const string CheckboxQuestionErrorMesseage = "Моля изберете най-малко един отговор на въпрос {0}!";

        private readonly IQuestionService _questionService;
        private readonly IParticipantAnswerService _participantAnswerService;
        
        public ParticipantFillOutController(
            IQuestionService questionService, 
            IParticipantAnswerService participantAnswerService)
        {
            _questionService = questionService;
            _participantAnswerService = participantAnswerService;
        }

        [HttpGet]
        public ActionResult FillOut(Guid voteSystemId)
        {
            var questionsAsViewModel = _questionService
                                            .GetQuestionsWithAnswersByVoteSystemId(voteSystemId)
                                            .To<ParticipantQuestionAnswerViewModel>()
                                            .ToList();

            return View(questionsAsViewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FillOut(IList<ParticipantQuestionAnswerViewModel> participantQuestionAnswers)
        {
            if (!ValidateFillOutRequest(participantQuestionAnswers))
            {
                return View(participantQuestionAnswers);
            }

            try
            {
                var userId = new Guid(User.Identity.GetUserId());
                var participantQuestionAnswersAsDto = participantQuestionAnswers.To<ParticipantQuestionAnswerDto>().ToList();

                _participantAnswerService.Add(participantQuestionAnswersAsDto, userId);

                this.AddNotification("Благодаря Ви, че гласувахте!", NotificationType.Success);
            }
            catch (Exception)
            {
                // TODO add login logic
                this.AddNotification("Възникна грешка по време на вашето гласувахте!", NotificationType.Error);
            }

            return this.RedirectToAction<DashboardController>(c => c.Index());
        }

        private bool ValidateFillOutRequest(IEnumerable<ParticipantQuestionAnswerViewModel> participantQuestionAnswers)
        {
            bool isValid = true;

            if (!ModelState.IsValid || participantQuestionAnswers == null)
            {
                isValid = false;
                return isValid;
            }

            foreach (var question in participantQuestionAnswers)
            {
                // question type - check box 
                if (question.HasMultipleAnswers)
                {
                    bool hasCheckedAnswer = question.Answers.Any(x => x.IsChecked);

                    if (!hasCheckedAnswer)
                    {
                        ModelState.AddModelError(question.Id.ToString(), string.Format(CheckboxQuestionErrorMesseage, question.Name));
                        isValid = false;
                    }
                }
                // question type - radio
                else
                {
                    if (question.RadioGroupParticipantAnswer == null)
                    {
                        ModelState.AddModelError(question.Id.ToString(), string.Format(CheckboxQuestionErrorMesseage, question.Name));
                        isValid = false;
                    }
                }
            }

            return isValid;
        }
    }
}