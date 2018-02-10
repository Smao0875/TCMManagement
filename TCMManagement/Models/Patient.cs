﻿using System.ComponentModel.DataAnnotations;

namespace TCMManagement.Models
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactPhone { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}