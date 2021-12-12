namespace Ankk.Models
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class User : IdentityUser
    {
        public User()
        {            
            this.Contests = new HashSet<Contest>();
            this.Answers = new HashSet<Answer>();
        }

        public int Fn { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public virtual ICollection<Contest> Contests { get; set; }

        //public int ContestId { get; set; }

        //public virtual Contest Contest { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
    }
}
