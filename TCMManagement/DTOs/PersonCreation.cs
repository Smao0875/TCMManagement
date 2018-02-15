using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace TCMManagement.Models
{
    public class PersonCreation
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime DateCreated { get; set; }
        public string Phone { get; set; }
        public string Note { get; set; }
        public string Password { get; set; }

        public int UserRoleId { get; set; }
    }
}