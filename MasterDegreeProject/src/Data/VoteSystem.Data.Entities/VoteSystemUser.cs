using System;
using System.Collections.Generic;

namespace VoteSystem.Data.Entities
{
    public class VoteSystemUser
    {
        public VoteSystemUser()
        {
            Participants = new HashSet<Participant>();
        }
        
        public Guid Id { get; set; }

        public int FacultyNumber { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<Participant> Participants { get; set; }
    }
}
