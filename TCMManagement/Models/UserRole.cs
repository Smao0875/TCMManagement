using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TCMManagement.Models
{
    public class UserRole
    {
        [Key]
        public int UserRoleId { get; set; }
        public int Role { get; set; }
        public string Description { get; set; }
    }
}