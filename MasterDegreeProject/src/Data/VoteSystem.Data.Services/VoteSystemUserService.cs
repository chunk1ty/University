using System;
using System.Collections.Generic;
using System.Linq;

using VoteSystem.Data.Contracts;
using VoteSystem.Data.Entities;
using VoteSystem.Data.Services.Contracts;

namespace VoteSystem.Data.Services
{
    public class VoteSystemUserService : IVoteSystemUserService
    {
        private readonly IVoteSystemUserRepository _voteSystemUserRepository;

        public VoteSystemUserService(IVoteSystemUserRepository voteSystemUserRepository)
        {
            _voteSystemUserRepository = voteSystemUserRepository;
        }
       
        public IEnumerable<VoteSystemUser> GetUnselectedVoteSystemUsersByVoteSystemId(Guid voteSystemId)
        {
            //var users = _voteSystemUserRepository
            //                                .GetWithParticipnats()
            //                                .Where(
            //                                    x => x.Participants
            //                                        .Any(y => y.VoteSystemId != voteSystemId));

            var users = _voteSystemUserRepository.GetWithParticipnats();

            var unselectedUsers = users.Except(GetSelectedUsers(voteSystemId));

            return unselectedUsers;
        }

        private IEnumerable<VoteSystemUser> GetSelectedUsers(Guid voteSystemId)
        {
            return _voteSystemUserRepository
                .GetWithParticipnats()
                .Where(
                    x => x.Participants
                        .Any(y => y.VoteSystemId == voteSystemId));
        }
    }
}
