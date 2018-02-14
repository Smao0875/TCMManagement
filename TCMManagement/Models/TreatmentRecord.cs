using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace TCMManagement.Models
{
    [Serializable]
    [DataContract(IsReference = true)]
    public class TreatmentRecord
    {
        [Key]
        public int TreatmentRecordId { get; set; }
        public DateTime DateCreated { get; set; }
        public string Symptom { get; set; }
        public string Diagnosis { get; set; }
        public int PrescriptionID { get; set; }
        public bool IsDeleted { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}