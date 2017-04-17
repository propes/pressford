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


            // Generate dummy articles.

            context.Articles.AddOrUpdate(
                new Article
                {
                    Id = 1,
                    AuthorId = publisher.Id,
                    DatePublished = new DateTime(2016, 10, 28),
                    DateModified = new DateTime(2016, 10, 28),
                    Title = "Lorem Ipsum",
                    Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin commodo mauris fermentum, tempor ante quis, imperdiet tortor. Nulla placerat augue libero, sed ultricies nisi pulvinar at. Phasellus ultricies eu eros id tristique. Proin facilisis nulla quam, id vulputate tellus rhoncus quis. Nunc enim enim, semper et posuere eu, efficitur in ante. In hac habitasse platea dictumst. Nullam fermentum, velit a efficitur volutpat, dui nisl congue massa, vel lobortis odio tellus eu libero. Phasellus vitae mauris non tellus semper rutrum. Nam ultrices neque scelerisque, pellentesque turpis ut, lacinia erat.\n\n" +
                           "Etiam nibh lorem, tempus vitae bibendum quis, imperdiet at felis.Ut malesuada gravida ipsum, eget elementum orci dapibus ac.Sed in metus viverra, imperdiet ligula sit amet, molestie ipsum.Nullam porttitor nunc molestie ipsum viverra, nec ultricies leo porta.Nunc lacus neque, fringilla nec placerat eleifend, dignissim vitae risus.Donec sed lectus eu quam fringilla mattis quis volutpat massa.Integer metus mi, lobortis finibus semper eget, interdum sed est.Integer mattis tellus vel odio bibendum interdum.Ut ex mi, suscipit vitae vulputate ac, maximus vel lectus.Maecenas quam dui, vehicula non ipsum vel, cursus mollis elit.Proin lorem leo, vehicula ac interdum et, pretium ac justo.\n\n" +
                           "Donec purus ipsum, aliquam id elit sollicitudin, efficitur finibus ligula.Vestibulum ultrices porttitor convallis.Morbi vulputate aliquet erat non tempus.Vestibulum pretium nibh a erat tincidunt egestas.Mauris dapibus ipsum nibh, non tristique urna euismod vitae.Curabitur lacinia arcu vitae aliquet scelerisque.Nulla eu iaculis odio.Curabitur et vehicula ante, at lacinia odio.\n\n" +
                           "Nullam imperdiet faucibus neque nec varius.Etiam rutrum rutrum tellus.Maecenas accumsan molestie dictum.Aliquam dapibus enim arcu, vel tincidunt magna dictum nec.Fusce viverra, ipsum quis sodales eleifend, nulla ex gravida libero, euismod laoreet ligula nibh ac nibh.Praesent non erat vel arcu ullamcorper mattis sit amet eu tortor.Donec luctus ultrices mollis.Fusce hendrerit purus eu massa dictum aliquet.Integer pharetra nunc et diam commodo sodales.Donec ullamcorper, ante ac ultricies consequat, velit eros ultrices leo, sed scelerisque orci felis eget arcu.Curabitur hendrerit nulla est, in porta lacus vestibulum vel.In cursus nisi at orci vestibulum, sit amet aliquam lectus lobortis.Fusce at ante scelerisque, commodo turpis quis, ullamcorper elit.Sed congue risus metus, vel facilisis nisi maximus nec.\n\n" +
                           "Morbi pretium ante eu lectus consectetur, id viverra nulla sollicitudin.Integer fringilla, neque nec bibendum feugiat, lacus libero rhoncus turpis, sed sagittis sem eros in ligula.In elementum hendrerit diam, cursus mollis massa volutpat at.Proin vel sollicitudin orci, sit amet maximus augue.Nullam non tempor neque.Nunc id felis sit amet mauris auctor consequat nec id diam.Ut eu tellus sit amet nisl gravida fermentum at vel tortor.Nunc vel pretium justo, vulputate scelerisque odio."
                },
                new Article
                {
                    Id = 2,
                    AuthorId = publisher.Id,
                    DatePublished = new DateTime(2017, 3, 14),
                    DateModified = new DateTime(2017, 3, 14),
                    Title = "Lorem Ipsum",
                    Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin commodo mauris fermentum, tempor ante quis, imperdiet tortor. Nulla placerat augue libero, sed ultricies nisi pulvinar at. Phasellus ultricies eu eros id tristique. Proin facilisis nulla quam, id vulputate tellus rhoncus quis. Nunc enim enim, semper et posuere eu, efficitur in ante. In hac habitasse platea dictumst. Nullam fermentum, velit a efficitur volutpat, dui nisl congue massa, vel lobortis odio tellus eu libero. Phasellus vitae mauris non tellus semper rutrum. Nam ultrices neque scelerisque, pellentesque turpis ut, lacinia erat.\n\n" + 
                           "Etiam nibh lorem, tempus vitae bibendum quis, imperdiet at felis.Ut malesuada gravida ipsum, eget elementum orci dapibus ac.Sed in metus viverra, imperdiet ligula sit amet, molestie ipsum.Nullam porttitor nunc molestie ipsum viverra, nec ultricies leo porta.Nunc lacus neque, fringilla nec placerat eleifend, dignissim vitae risus.Donec sed lectus eu quam fringilla mattis quis volutpat massa.Integer metus mi, lobortis finibus semper eget, interdum sed est.Integer mattis tellus vel odio bibendum interdum.Ut ex mi, suscipit vitae vulputate ac, maximus vel lectus.Maecenas quam dui, vehicula non ipsum vel, cursus mollis elit.Proin lorem leo, vehicula ac interdum et, pretium ac justo.\n\n" +
                           "Donec purus ipsum, aliquam id elit sollicitudin, efficitur finibus ligula.Vestibulum ultrices porttitor convallis.Morbi vulputate aliquet erat non tempus.Vestibulum pretium nibh a erat tincidunt egestas.Mauris dapibus ipsum nibh, non tristique urna euismod vitae.Curabitur lacinia arcu vitae aliquet scelerisque.Nulla eu iaculis odio.Curabitur et vehicula ante, at lacinia odio.\n\n" +
                           "Nullam imperdiet faucibus neque nec varius.Etiam rutrum rutrum tellus.Maecenas accumsan molestie dictum.Aliquam dapibus enim arcu, vel tincidunt magna dictum nec.Fusce viverra, ipsum quis sodales eleifend, nulla ex gravida libero, euismod laoreet ligula nibh ac nibh.Praesent non erat vel arcu ullamcorper mattis sit amet eu tortor.Donec luctus ultrices mollis.Fusce hendrerit purus eu massa dictum aliquet.Integer pharetra nunc et diam commodo sodales.Donec ullamcorper, ante ac ultricies consequat, velit eros ultrices leo, sed scelerisque orci felis eget arcu.Curabitur hendrerit nulla est, in porta lacus vestibulum vel.In cursus nisi at orci vestibulum, sit amet aliquam lectus lobortis.Fusce at ante scelerisque, commodo turpis quis, ullamcorper elit.Sed congue risus metus, vel facilisis nisi maximus nec.\n\n" +
                           "Morbi pretium ante eu lectus consectetur, id viverra nulla sollicitudin.Integer fringilla, neque nec bibendum feugiat, lacus libero rhoncus turpis, sed sagittis sem eros in ligula.In elementum hendrerit diam, cursus mollis massa volutpat at.Proin vel sollicitudin orci, sit amet maximus augue.Nullam non tempor neque.Nunc id felis sit amet mauris auctor consequat nec id diam.Ut eu tellus sit amet nisl gravida fermentum at vel tortor.Nunc vel pretium justo, vulputate scelerisque odio."
                },
                new Article
                {
                    Id = 3,
                    AuthorId = publisher.Id,
                    DatePublished = new DateTime(2017, 4, 10),
                    DateModified = new DateTime(2017, 4, 10),
                    Title = "Lorem Ipsum",
                    Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin commodo mauris fermentum, tempor ante quis, imperdiet tortor. Nulla placerat augue libero, sed ultricies nisi pulvinar at. Phasellus ultricies eu eros id tristique. Proin facilisis nulla quam, id vulputate tellus rhoncus quis. Nunc enim enim, semper et posuere eu, efficitur in ante. In hac habitasse platea dictumst. Nullam fermentum, velit a efficitur volutpat, dui nisl congue massa, vel lobortis odio tellus eu libero. Phasellus vitae mauris non tellus semper rutrum. Nam ultrices neque scelerisque, pellentesque turpis ut, lacinia erat.\n\n" +
                           "Etiam nibh lorem, tempus vitae bibendum quis, imperdiet at felis.Ut malesuada gravida ipsum, eget elementum orci dapibus ac.Sed in metus viverra, imperdiet ligula sit amet, molestie ipsum.Nullam porttitor nunc molestie ipsum viverra, nec ultricies leo porta.Nunc lacus neque, fringilla nec placerat eleifend, dignissim vitae risus.Donec sed lectus eu quam fringilla mattis quis volutpat massa.Integer metus mi, lobortis finibus semper eget, interdum sed est.Integer mattis tellus vel odio bibendum interdum.Ut ex mi, suscipit vitae vulputate ac, maximus vel lectus.Maecenas quam dui, vehicula non ipsum vel, cursus mollis elit.Proin lorem leo, vehicula ac interdum et, pretium ac justo.\n\n" +
                           "Donec purus ipsum, aliquam id elit sollicitudin, efficitur finibus ligula.Vestibulum ultrices porttitor convallis.Morbi vulputate aliquet erat non tempus.Vestibulum pretium nibh a erat tincidunt egestas.Mauris dapibus ipsum nibh, non tristique urna euismod vitae.Curabitur lacinia arcu vitae aliquet scelerisque.Nulla eu iaculis odio.Curabitur et vehicula ante, at lacinia odio.\n\n" +
                           "Nullam imperdiet faucibus neque nec varius.Etiam rutrum rutrum tellus.Maecenas accumsan molestie dictum.Aliquam dapibus enim arcu, vel tincidunt magna dictum nec.Fusce viverra, ipsum quis sodales eleifend, nulla ex gravida libero, euismod laoreet ligula nibh ac nibh.Praesent non erat vel arcu ullamcorper mattis sit amet eu tortor.Donec luctus ultrices mollis.Fusce hendrerit purus eu massa dictum aliquet.Integer pharetra nunc et diam commodo sodales.Donec ullamcorper, ante ac ultricies consequat, velit eros ultrices leo, sed scelerisque orci felis eget arcu.Curabitur hendrerit nulla est, in porta lacus vestibulum vel.In cursus nisi at orci vestibulum, sit amet aliquam lectus lobortis.Fusce at ante scelerisque, commodo turpis quis, ullamcorper elit.Sed congue risus metus, vel facilisis nisi maximus nec.\n\n" +
                           "Morbi pretium ante eu lectus consectetur, id viverra nulla sollicitudin.Integer fringilla, neque nec bibendum feugiat, lacus libero rhoncus turpis, sed sagittis sem eros in ligula.In elementum hendrerit diam, cursus mollis massa volutpat at.Proin vel sollicitudin orci, sit amet maximus augue.Nullam non tempor neque.Nunc id felis sit amet mauris auctor consequat nec id diam.Ut eu tellus sit amet nisl gravida fermentum at vel tortor.Nunc vel pretium justo, vulputate scelerisque odio."
                },
                new Article
                {
                    Id = 4,
                    AuthorId = publisher.Id,
                    DatePublished = new DateTime(2017, 4, 17),
                    DateModified = new DateTime(2017, 4, 17),
                    Title = "Lorem Ipsum",
                    Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin commodo mauris fermentum, tempor ante quis, imperdiet tortor. Nulla placerat augue libero, sed ultricies nisi pulvinar at. Phasellus ultricies eu eros id tristique. Proin facilisis nulla quam, id vulputate tellus rhoncus quis. Nunc enim enim, semper et posuere eu, efficitur in ante. In hac habitasse platea dictumst. Nullam fermentum, velit a efficitur volutpat, dui nisl congue massa, vel lobortis odio tellus eu libero. Phasellus vitae mauris non tellus semper rutrum. Nam ultrices neque scelerisque, pellentesque turpis ut, lacinia erat.\n\n" +
                           "Etiam nibh lorem, tempus vitae bibendum quis, imperdiet at felis.Ut malesuada gravida ipsum, eget elementum orci dapibus ac.Sed in metus viverra, imperdiet ligula sit amet, molestie ipsum.Nullam porttitor nunc molestie ipsum viverra, nec ultricies leo porta.Nunc lacus neque, fringilla nec placerat eleifend, dignissim vitae risus.Donec sed lectus eu quam fringilla mattis quis volutpat massa.Integer metus mi, lobortis finibus semper eget, interdum sed est.Integer mattis tellus vel odio bibendum interdum.Ut ex mi, suscipit vitae vulputate ac, maximus vel lectus.Maecenas quam dui, vehicula non ipsum vel, cursus mollis elit.Proin lorem leo, vehicula ac interdum et, pretium ac justo.\n\n" +
                           "Donec purus ipsum, aliquam id elit sollicitudin, efficitur finibus ligula.Vestibulum ultrices porttitor convallis.Morbi vulputate aliquet erat non tempus.Vestibulum pretium nibh a erat tincidunt egestas.Mauris dapibus ipsum nibh, non tristique urna euismod vitae.Curabitur lacinia arcu vitae aliquet scelerisque.Nulla eu iaculis odio.Curabitur et vehicula ante, at lacinia odio.\n\n" +
                           "Nullam imperdiet faucibus neque nec varius.Etiam rutrum rutrum tellus.Maecenas accumsan molestie dictum.Aliquam dapibus enim arcu, vel tincidunt magna dictum nec.Fusce viverra, ipsum quis sodales eleifend, nulla ex gravida libero, euismod laoreet ligula nibh ac nibh.Praesent non erat vel arcu ullamcorper mattis sit amet eu tortor.Donec luctus ultrices mollis.Fusce hendrerit purus eu massa dictum aliquet.Integer pharetra nunc et diam commodo sodales.Donec ullamcorper, ante ac ultricies consequat, velit eros ultrices leo, sed scelerisque orci felis eget arcu.Curabitur hendrerit nulla est, in porta lacus vestibulum vel.In cursus nisi at orci vestibulum, sit amet aliquam lectus lobortis.Fusce at ante scelerisque, commodo turpis quis, ullamcorper elit.Sed congue risus metus, vel facilisis nisi maximus nec.\n\n" +
                           "Morbi pretium ante eu lectus consectetur, id viverra nulla sollicitudin.Integer fringilla, neque nec bibendum feugiat, lacus libero rhoncus turpis, sed sagittis sem eros in ligula.In elementum hendrerit diam, cursus mollis massa volutpat at.Proin vel sollicitudin orci, sit amet maximus augue.Nullam non tempor neque.Nunc id felis sit amet mauris auctor consequat nec id diam.Ut eu tellus sit amet nisl gravida fermentum at vel tortor.Nunc vel pretium justo, vulputate scelerisque odio."
                }
            );


            context.SaveChanges();
        }
    }
}
