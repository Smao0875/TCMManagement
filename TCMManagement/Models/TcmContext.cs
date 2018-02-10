using System.Data.Entity;

namespace TCMManagement.Models
{
    public class TcmContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<UserRole> Roles { get; set; }
        public DbSet<Patient> Patients { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}