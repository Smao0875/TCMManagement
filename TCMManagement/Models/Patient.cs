using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TCMManagement.Models
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime DateCreated { get; set; }
        public string Phone { get; set; }
        public string Note { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactPhone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsDeleted { get; set; }

        public int UserRoleId { get; set; }
        public UserRole Role { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<TreatmentRecord> TreatmentRecords { get; set; }
        public ICollection<MedicalHistoryRecord> MedicalHistoryRecords { get; set; }
    }
}