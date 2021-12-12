using System;
using System.Collections.Generic;

namespace VotySystem.Data.DTO
{
    public class ParticipantQuestionAnswerDto
    {
        public ParticipantQuestionAnswerDto()
        {
            Answers = new List<ParticipantAnswerDto>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        // dummy way to detect user answer when question has radio group
        public string RadioGroupParticipantAnswer { get; set; }

        public bool HasMultipleAnswers { get; set; }

        public Guid VoteSystemId { get; set; }

        public IList<ParticipantAnswerDto> Answers { get; set; }
    }
}
