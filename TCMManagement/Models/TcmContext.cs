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
            base.OnModelCreating(modelBuilder);
        }
    }
}