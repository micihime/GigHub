using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace GigHub.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Gig> Gigs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Attendance> Attendances { get; set; } //in order to be able to query this db table
        public DbSet<Following> Followings { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) //overriding this method in order to be able to use Fluent API
        {
            //getting rid of ERROR when updating the DB
            //  Introducing FOREIGN KEY constraint 'FK_dbo.Attendances_dbo.Gigs_GigId' on table 'Attendances' may cause cycles
            //  or multiple cascade paths. Specify ON DELETE NO ACTION or ON UPDATE NO ACTION, or modify other FOREIGN KEY
            //  constraints.
            //removing the cascade on delete for Gigs and Attendance table
            modelBuilder.Entity<Attendance>().
                HasRequired(a => a.Gig)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Following>().
                HasRequired(a => a.Follower)
                .WithMany()
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}