using System;
using System.Collections.Generic;
using System.Linq;

using VoteSystem.Clients.MVC.Areas.Administration.ViewModels.VoteSystemUser;
using VoteSystem.Clients.MVC.Infrastructure.Mapping.Contracts;
using VotySystem.Data.DTO;

namespace VoteSystem.Clients.MVC.Areas.Administration.ViewModels.VoteSystem
{
    public class VoteSystemParticipantsViewModel : IMapTo<VoteSystemParticipantsDto>
    {
        public VoteSystemParticipantsViewModel()
        {
            VoteSystemUsers = new List<VoteSystemUserViewModel>();
        }

        public Guid VoteSystemId { get; set; }

        public IList<VoteSystemUserViewModel> VoteSystemUsers { get; set; }

        public IList<VoteSystemUserViewModel> SelectedVoteSystemUsers
        {
            get
            {
                return VoteSystemUsers.Where(x => x.IsSelected).ToList();
            }
        }
    }
}