using System;
using System.ComponentModel.DataAnnotations;

namespace TCMManagement.Models
{
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

        #region Billing part
        public double TotalAmount { get; set; }
        public double Change { get; set; }
        public double AmountPaid { get; set; }
        public string PaymentMethod { get; set; }
        #endregion
    }
}