using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace TCMManagement.Models
{
    [Serializable]
    [DataContract(IsReference = true)]
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public DateTime DateCreated { get; set; }
        public string Description { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}