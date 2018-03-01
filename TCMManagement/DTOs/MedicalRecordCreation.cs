using System;

namespace TCMManagement.DTOs
{
    public class MedicalRecordCreation
    {
        public DateTime DateCreated { get; set; }
        public string Description { get; set; }
        public string Medication { get; set; }
        public string Duration { get; set; }
        public string Dosage { get; set; }
        public bool IsFamily { get; set; }
        public int PatientId { get; set; }
    }
}