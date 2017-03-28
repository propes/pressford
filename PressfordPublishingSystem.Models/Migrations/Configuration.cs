namespace PressfordPublishingSystem.Models.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PressfordPublishingSystem.Models.PublisherDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PressfordPublishingSystem.Models.PublisherDbContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);


            // Add Roles.

            if (!context.Roles.Any(r => r.Name == UserRole.Publisher))
            {
                roleManager.Create(new IdentityRole { Name = UserRole.Publisher });
            }

            if (!context.Roles.Any(r => r.Name == UserRole.Employee))
            {
                roleManager.Create(new IdentityRole { Name = UserRole.Employee });
            }

            context.SaveChanges();

            // Add Test Users.

            var userStore = new UserStore<User>(context);
            var userManager = new UserManager<User>(userStore);

            var publisher = context.Users.SingleOrDefault(u => u.UserName == "publisher");
            if (publisher == null)
            {
                publisher = new User { UserName = "publisher" };

                userManager.Create(publisher, AppConstants.DefaultPassword);
                userManager.AddToRole(publisher.Id, UserRole.Publisher);
            }

            var employee = context.Users.SingleOrDefault(u => u.UserName == "employee");
            if (employee == null)
            {
                employee = new User { UserName = "employee" };

                userManager.Create(employee, AppConstants.DefaultPassword);
                userManager.AddToRole(employee.Id, UserRole.Employee);
            }

            context.SaveChanges();
        }
    }
}
