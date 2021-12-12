using System;
using System.Collections.Generic;
using System.Linq;

using VoteSystem.Data.Contracts;
using VoteSystem.Data.Entities;
using VoteSystem.Data.Services.Contracts;
using VotySystem.Data.DTO;

namespace VoteSystem.Data.Services
{
    public class ParticipantAnswerService : IParticipantAnswerService
    {
        private readonly IParticipantAnswerRepository _participantAnswerRepository;
        private readonly IParticipantService _participantService;
        private readonly IVoteSystemEfDbContextSaveChanges _dbContextSaveChanges;

        public ParticipantAnswerService(
            IParticipantAnswerRepository participantAnswerRepository, 
            IVoteSystemEfDbContextSaveChanges dbContextSaveChanges,
            IParticipantService participantService)
        {
            _participantAnswerRepository = participantAnswerRepository;
            _dbContextSaveChanges = dbContextSaveChanges;
            _participantService = participantService;
        }

        public void Add(IList<ParticipantQuestionAnswerDto> participantQuestionAnswers, Guid userId)
        {
            if (participantQuestionAnswers == null)
            {
                throw new ArgumentNullException(nameof(participantQuestionAnswers));
            }

            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            var voteSystemId = participantQuestionAnswers[0].VoteSystemId;
            var participant = _participantService.GetParticipantByVoteSystemIdAndVoteSystemUserId(voteSystemId, userId);

            foreach (var question in participantQuestionAnswers)
            {
                // TODO use factory pattern
                var currentParticipantAnswer = new ParticipantAnswer();

                if (question.HasMultipleAnswers)
                {
                    foreach (var answer in question.Answers.Where(x => x.IsChecked))
                    {
                        currentParticipantAnswer.AnswerId = answer.Id;
                        currentParticipantAnswer.ParticipantId = participant.Id;

                        _participantAnswerRepository.Add(currentParticipantAnswer);

                        // TODO use factory pattern
                        currentParticipantAnswer = new ParticipantAnswer();
                    }
                }
                else
                {
                    currentParticipantAnswer.AnswerId = int.Parse(question.RadioGroupParticipantAnswer);
                    currentParticipantAnswer.ParticipantId = participant.Id;

                    _participantAnswerRepository.Add(currentParticipantAnswer);
                }
            }

            participant.IsVoted = true;
            _participantService.Update(participant);

            _dbContextSaveChanges.SaveChanges();
        }
    }
}
