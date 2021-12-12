using System;
using System.Security.Claims;
using System.Threading.Tasks; 

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

using VoteSystem.Data.Entities;

namespace VoteSystem.Data.Ef.Models
{
    public class AspNetUser : IdentityUser
    {
        // TODO remove FacultyNumber FirstName LastName properties
        public int FacultyNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Guid? VoteSystemUserId { get; set; }

        public virtual VoteSystemUser VoteSystemUser { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AspNetUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
          
            return userIdentity;
        }
    }
}
