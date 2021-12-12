using VoteSystem.Clients.MVC.Infrastructure.Mapping.Contracts;
using VotySystem.Data.DTO;

namespace VoteSystem.Clients.MVC.ViewModels.ParticipantFillOut
{
    public class ParticipantAnswerViewModel : IMapFrom<Data.Entities.Answer>, IMapTo<Data.Entities.Answer>, IMapTo<ParticipantAnswerDto>
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public bool IsChecked { get; set; }
    }
}