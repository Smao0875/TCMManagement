using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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

        public int PersonId { get; set; }
        public Person Person { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}