using System;
using System.ComponentModel.DataAnnotations;

namespace TCMManagement.Models
{
    public class MedicalHistoryRecord
    {
        [Key]
        public int MedicalHistoryRecordId { get; set; }
        public DateTime DateCreated { get; set; }
        public string Description { get; set; }
        public string Medication { get; set; }
        public string Duration { get; set; }
        public string Dosage { get; set; }
        public bool IsFamily { get; set; }

        public bool IsDeleted { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}