using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressfordPublishingSystem.Models
{
    public class PublisherDbContext : IdentityDbContext<User>
    {
        public PublisherDbContext() : base("name=DefaultConnection", throwIfV1Schema: false)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Disable cascade delete by default for one-to-many relationships.
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }

        public static PublisherDbContext Create()
        {
            return new PublisherDbContext();
        }

        public virtual DbSet<Article> Articles { get; set; }
    }
}
