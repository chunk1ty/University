using System;

using VoteSystem.Clients.MVC.Infrastructure.Mapping.Contracts;

namespace VoteSystem.Clients.MVC.Areas.Administration.ViewModels.VoteSystemUser
{
    public class VoteSystemUserViewModel : IMapFrom<Data.Entities.VoteSystemUser>, IMapTo<Data.Entities.VoteSystemUser>
    {
        public Guid Id { get; set; }

        public int FacultyNumber { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsVoted { get; set; }

        public bool IsSelected { get; set; }
    }
}