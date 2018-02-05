﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TCMManagement.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public DateTime DateCreated { get; set; }
        public string Description { get; set; }


        public int PatientId { get; set; }
        public int PersonId { get; set; }
        public Patient Patient { get; set; }
        public Person Practitioner { get; set; }
    }
}