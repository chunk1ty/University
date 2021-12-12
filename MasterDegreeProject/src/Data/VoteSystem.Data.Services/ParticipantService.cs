using System;
using System.Collections.Generic;
using System.Linq;

using VoteSystem.Data.Contracts;
using VoteSystem.Data.Entities;
using VoteSystem.Data.Services.Contracts;
using VotySystem.Data.DTO;

namespace VoteSystem.Data.Services
{
    public class ParticipantService : IParticipantService
    {
        private readonly IParticipantRepository _participantRepository;
        private readonly IVoteSystemEfDbContextSaveChanges _dbContextSaveChanges;

        public ParticipantService(IParticipantRepository participantRepository, IVoteSystemEfDbContextSaveChanges dbContextSaveChanges)
        {
            _participantRepository = participantRepository;
            _dbContextSaveChanges = dbContextSaveChanges;
        }

        public void AddRange(VoteSystemParticipantsDto voteSystemParticipants)
        {
            var voteSystemId = voteSystemParticipants.VoteSystemId;

            foreach (var user in voteSystemParticipants.SelectedVoteSystemUsers)
            {
                var currentParticipant = new Participant()
                {
                    VoteSystemId = voteSystemId,
                    VoteSystemUserId = user.Id
                };

                _participantRepository.Add(currentParticipant);
            }

            _dbContextSaveChanges.SaveChanges();
        }

        public void Update(Participant participant)
        {
            _participantRepository.Update(participant);

            _dbContextSaveChanges.SaveChanges();
        }

        public void RemoveRange(IEnumerable<Participant> participants)
        {
            foreach (var participant in participants)
            {
                _participantRepository.Delete(participant);
            }

            _dbContextSaveChanges.SaveChanges();
        }

        public Participant GetParticipantByVoteSystemIdAndVoteSystemUserId(Guid voteSystemId, Guid userId)
        {
            var participant = _participantRepository
                                                .All()
                                                .FirstOrDefault(x => x.VoteSystemId == voteSystemId && x.VoteSystemUserId == userId);

            return participant;
        }

        public IEnumerable<Participant> GetParticipantsByVoteSystemId(Guid voteSystemId)
        {
            return _participantRepository
                                    .AllWithVoteSystemUser()
                                    .Where(x => x.VoteSystemId == voteSystemId);
        }
    }
}
