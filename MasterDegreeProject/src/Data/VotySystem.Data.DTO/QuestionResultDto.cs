using System.Collections.Generic;

namespace VotySystem.Data.DTO
{
    public class QuestionResultDto
    {
        public QuestionResultDto()
        {
            Answers = new HashSet<AnswerResultDto>();
        }

        public string Name { get; set; }

        public bool Type { get; set; }

        public IEnumerable<AnswerResultDto> Answers { get; set; }
    }
}