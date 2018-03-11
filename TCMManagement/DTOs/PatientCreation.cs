using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TCMManagement.DTOs
{
    public class PatientCreation
    {
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
    }
}