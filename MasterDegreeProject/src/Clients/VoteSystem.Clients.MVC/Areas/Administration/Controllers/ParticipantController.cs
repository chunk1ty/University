using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Expressions;

using VoteSystem.Clients.MVC.Areas.Administration.ViewModels.Participant;
using VoteSystem.Clients.MVC.Areas.Administration.ViewModels.VoteSystem;
using VoteSystem.Clients.MVC.Areas.Administration.ViewModels.VoteSystemUser;
using VoteSystem.Clients.MVC.Infrastructure.Attributes;
using VoteSystem.Clients.MVC.Infrastructure.Mapping;
using VoteSystem.Clients.MVC.Infrastructure.NotificationSystem;
using VoteSystem.Common.Constants;
using VoteSystem.Data.Entities;
using VoteSystem.Data.Services.Contracts;
using VotySystem.Data.DTO;

namespace VoteSystem.Clients.MVC.Areas.Administration.Controllers
{
    public class ParticipantController : AdminController
    {
        private readonly IVoteSystemUserService _voteSystemUserService;
        private readonly IParticipantService _participantService;

        public ParticipantController(
            IVoteSystemUserService voteSystemUserService,
            IParticipantService participantService)
        {
            _voteSystemUserService = voteSystemUserService ?? throw new ArgumentNullException(nameof(voteSystemUserService));
            _participantService = participantService ?? throw new ArgumentNullException(nameof(participantService));
        }

        [HttpGet]
        public ActionResult Add(Guid voteSystemId)
        {
            var voteSystemUsers = _voteSystemUserService
                                                    .GetUnselectedVoteSystemUsersByVoteSystemId(voteSystemId)
                                                    .To<VoteSystemUserViewModel>()
                                                    .ToList();

            var voteSystemParticipantsViewModel = new VoteSystemParticipantsViewModel
            {
                VoteSystemUsers = voteSystemUsers,
                VoteSystemId = voteSystemId
            };

            return View(voteSystemParticipantsViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(VoteSystemParticipantsViewModel model)
        {
            if (!ValidateAddParticipantRequest(model))
            {
                return View(model);
            }

            try
            {
                var voteSystemParticipantsAsDto = Mapper.Map<VoteSystemParticipantsDto>(model);
                _participantService.AddRange(voteSystemParticipantsAsDto);
              
                this.AddNotification("Успешно добавихте учасници!", NotificationType.Success);
            }
            catch (Exception)
            {
                //TODO add login logic
                this.AddNotification("Възникна грешка при добавянето на учасниците!", NotificationType.Error);
            }

            return this.RedirectToAction<VoteSystemController>(x => x.Index());
        }

        [HttpGet]
        public ActionResult Remove(Guid voteSystemId)
        {
            var participants = _participantService
                                                .GetParticipantsByVoteSystemId(voteSystemId)
                                                .To<ParticipantViewModel>()
                                                .ToList();

            return View(participants);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Remove(IList<ParticipantViewModel> participants)
        {
            if (!ValidateRemoveParticipantRequest(participants))
            {
                return View(participants);
            }

            try
            {
                var participantsAsDbEntities = participants
                                                        .Where(x => x.VoteSystemUser.IsSelected)
                                                        .To<Participant>();

                _participantService.RemoveRange(participantsAsDbEntities);

                this.AddNotification("Успешно премахнахте учасници!", NotificationType.Success);
            }
            catch (Exception)
            {
                //TODO add login logic
                this.AddNotification("Възникна грешка при премахването на учасниците!", NotificationType.Error);
            }

            return this.RedirectToAction<VoteSystemController>(x => x.Index());
        }

        [HttpGet]
        public ActionResult Preview(Guid voteSystemId)
        {
            var participants = _participantService
                                            .GetParticipantsByVoteSystemId(voteSystemId)
                                            .To<ParticipantViewModel>()
                                            .ToList();

            ViewBag.VoteSystemId = voteSystemId;

            return View(participants);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult SentEmails(Guid voteSystemId)
        {
            // TODO implement email service
            this.AddNotification("Успешно изпратихте имейли на всички учасници!", NotificationType.Success);

            return PartialView(PartialViewConstants.SuccessNotificationPartial);
        }

        private bool ValidateAddParticipantRequest(VoteSystemParticipantsViewModel model)
        {
            var IsValid = true;

            if (!ModelState.IsValid || model == null)
            {
                IsValid = false;
            }

            if (!model.SelectedVoteSystemUsers.Any())
            {
                ModelState.AddModelError(string.Empty, "Трябва да изберете най-малко един учасник.");
                IsValid = false;
            }

            return IsValid;
        }

        private bool ValidateRemoveParticipantRequest(IEnumerable<ParticipantViewModel> model)
        {
            var IsValid = true;

            if (!ModelState.IsValid || model == null)
            {
                IsValid = false;
            }

            var selectedParticipants = model.Where(x => x.VoteSystemUser.IsSelected);

            if (!selectedParticipants.Any())
            {
                ModelState.AddModelError(string.Empty, "Трябва да изберете най-малко един учасник.");
                IsValid = false;
            }

            return IsValid;
        }
    }
}