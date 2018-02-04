using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TCMManagement.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}