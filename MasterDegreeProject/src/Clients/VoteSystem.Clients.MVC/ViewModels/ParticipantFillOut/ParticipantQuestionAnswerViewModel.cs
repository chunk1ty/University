using System;
using System.Collections.Generic;

using VoteSystem.Clients.MVC.Infrastructure.Mapping.Contracts;
using VoteSystem.Data.Entities;
using VotySystem.Data.DTO;

namespace VoteSystem.Clients.MVC.ViewModels.ParticipantFillOut
{
    public class ParticipantQuestionAnswerViewModel : IMapFrom<Question>, IMapTo<Question>, IMapTo<ParticipantQuestionAnswerDto>
    {
        public ParticipantQuestionAnswerViewModel()
        {
            Answers = new List<ParticipantAnswerViewModel>();
        }

        public int Id { get; set; }

        public string Name { get; set; }
        
        // dummy way to detect participant answer when the question type is radio
        public string RadioGroupParticipantAnswer { get; set; }

        public bool HasMultipleAnswers { get; set; }

        public Guid VoteSystemId { get; set; }

        public IList<ParticipantAnswerViewModel> Answers { get; set; }
    }
}