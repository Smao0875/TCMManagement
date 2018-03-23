using System;

namespace TCMManagement.DTOs
{
    public class TreatmentCreation
    {
        public DateTime DateCreated { get; set; }
        public string Symptom { get; set; }
        public string Diagnosis { get; set; }
        public int PrescriptionID { get; set; }
        public int PersonId { get; set; }
        public int PatientId { get; set; }
    }
}