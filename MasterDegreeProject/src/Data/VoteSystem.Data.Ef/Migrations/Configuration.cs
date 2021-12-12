using System;
using System.Data.Entity.Migrations;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using VoteSystem.Common.Constants;
using VoteSystem.Data.Ef.Models;
using VoteSystem.Data.Entities;

namespace VoteSystem.Data.Ef.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<VoteSystemDbContext>
    {
        public Configuration()
        {
            // set to false in production and create migration by my self
           AutomaticMigrationsEnabled = true;
           AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(VoteSystemDbContext context)
        {
           CreateAdministrator(context);
           CreateUsers(context, 20);
        }

        private void CreateAdministrator(VoteSystemDbContext context)
        {
            const string administratorUserName = "admin@admin.com";
            const string administratorPassword = "123456";
           
            if (!context.Roles.Any())
            {
                // Create admin role
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var role = new IdentityRole { Name = GlobalConstants.AdministratorRoleName };
                roleManager.Create(role);

                // Create admin user
                var userStore = new UserStore<AspNetUser>(context);
                var userManager = new UserManager<AspNetUser>(userStore);
                var user = new AspNetUser
                {
                    UserName = administratorUserName,
                    Email = administratorUserName,
                    FacultyNumber = 10001,
                    FirstName = "Admin",
                    LastName = "Admin",
                    EmailConfirmed = true
                };
                userManager.Create(user, administratorPassword);

                // Assign user to admin role
                userManager.AddToRole(user.Id, GlobalConstants.AdministratorRoleName);
            }
        }

        private void CreateUsers(VoteSystemDbContext context, int numberOfUsers)
        {
            if (!context.VoteSystemUsers.Any())
            {
                const string userPassword = "123456";

                for (int i = 0; i < numberOfUsers; i++)
                {
                    var userStore = new UserStore<AspNetUser>(context);
                    var userManager = new UserManager<AspNetUser>(userStore);
                    var user = new AspNetUser
                    {
                        UserName = "user" + i + "@abv.bg",
                        Email = "user" + i + "@abv.bg",
                        FacultyNumber = 1000 + i,
                        FirstName = "FirstUserName" + i,
                        LastName = "LastUserName" + i,
                        EmailConfirmed = true
                    };

                    var voteSystemUser = new VoteSystemUser()
                    {
                        Id = Guid.NewGuid(),
                        Email = "user" + i + "@abv.bg",
                        FacultyNumber = 1000 + i,
                        FirstName = "FirstUserName" + i,
                        LastName = "LastUserName" + i,
                    };

                    userManager.Create(user, userPassword);

                    context.VoteSystemUsers.Add(voteSystemUser);
                }

                var admin = context.Users.FirstOrDefault(x => x.UserName == "admin@admin.com");

                var voteSystemAdminUser = new VoteSystemUser()
                {
                    Email = admin.Email,
                    FacultyNumber = admin.FacultyNumber,
                    Id = new Guid(admin.Id),
                    FirstName = "Admin",
                    LastName = "Admin"
                };

                context.VoteSystemUsers.Add(voteSystemAdminUser);

                context.SaveChanges();
            }
        }
    }
}
