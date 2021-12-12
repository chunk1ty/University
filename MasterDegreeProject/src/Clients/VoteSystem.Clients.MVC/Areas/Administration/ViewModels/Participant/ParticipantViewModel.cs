using VoteSystem.Clients.MVC.Areas.Administration.ViewModels.VoteSystemUser;
using VoteSystem.Clients.MVC.Infrastructure.Mapping.Contracts;

namespace VoteSystem.Clients.MVC.Areas.Administration.ViewModels.Participant
{
    public class ParticipantViewModel : IMapFrom<Data.Entities.Participant>, IMapTo<Data.Entities.Participant>
    {
        public int Id { get; set; }

        public VoteSystemUserViewModel VoteSystemUser { get; set; }
    }
}