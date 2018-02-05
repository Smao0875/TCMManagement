using System.Data.Entity;
using TCMManagement.Models;

namespace TCMManagement.DataAccessLayer
{
    public class TcmDAL : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<UserRole> Roles { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Patient> Patients { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("TblPerson");
            base.OnModelCreating(modelBuilder);
        }
    }
}