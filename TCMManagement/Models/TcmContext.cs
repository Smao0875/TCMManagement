using System.Data.Entity;

namespace TCMManagement.Models
{
    public class TcmContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<UserRole> Roles { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<TreatmentRecord> TreatmentRecords { get; set; }
        public DbSet<MedicalHistoryRecord> MedicalHistoryRecords { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                        .Map(m => m.Requires("IsDeleted").HasValue(false))
                        .Ignore(m => m.IsDeleted);
            modelBuilder.Entity<Patient>()
                        .Map(m => m.Requires("IsDeleted").HasValue(false))
                        .Ignore(m => m.IsDeleted);
            modelBuilder.Entity<TreatmentRecord>()
                        .Map(m => m.Requires("IsDeleted").HasValue(false))
                        .Ignore(m => m.IsDeleted);
            modelBuilder.Entity<MedicalHistoryRecord>()
                        .Map(m => m.Requires("IsDeleted").HasValue(false))
                        .Ignore(m => m.IsDeleted);
            base.OnModelCreating(modelBuilder);
        }
    }
}