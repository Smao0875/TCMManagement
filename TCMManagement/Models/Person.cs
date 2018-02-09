using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCMManagement.Models
{
    public class Person
    {
        [Key]
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
        public string Password { get; set; }

        public int UserRoleId { get; set; }
        public UserRole Role { get; set; }

        public ICollection<Appointment> Schedule { get; set; }
        public ICollection<Patient> Patients { get; set; }
    }
}