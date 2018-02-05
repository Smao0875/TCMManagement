namespace TCMManagement.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TCMManagement.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TCMManagement.DataAccessLayer.TcmDAL>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "TCMManagement.DataAccessLayer.TcmDAL";
        }

        protected override void Seed(TCMManagement.DataAccessLayer.TcmDAL context)
        {
            context.Roles.AddOrUpdate(x => x.UserRoleId,
                new UserRole() { UserRoleId = 1, Role = 1, Description = "Patient", DateCreated = DateTime.Now },
                new UserRole() { UserRoleId = 2, Role = 2, Description = "Practitioner", DateCreated = DateTime.Now },
                new UserRole() { UserRoleId = 3, Role = 3, Description = "Receptionist", DateCreated = DateTime.Now },
                new UserRole() { UserRoleId = 4, Role = 4, Description = "Admin", DateCreated = DateTime.Now }
            );

            context.People.AddOrUpdate(x => x.PersonId,
                new Person() { PersonId = 1, UserRoleId = 1, FirstName = "Patient", LastName = "1"},
                new Person() { PersonId = 2, UserRoleId = 1, FirstName = "Patient", LastName = "2" },
                new Person() { PersonId = 3, UserRoleId = 2, FirstName = "Practitioner", LastName = "1" },
                new Person() { PersonId = 4, UserRoleId = 2, FirstName = "Practitioner", LastName = "2" },
                new Person() { PersonId = 5, UserRoleId = 3, FirstName = "Receptionist", LastName = "1" },
                new Person() { PersonId = 6, UserRoleId = 4, FirstName = "Admin", LastName = "1" }
            );

            context.Patients.AddOrUpdate(x => x.PatientId,
                new Patient() { PatientId = 1, PersonId = 1, Phone = "111", Address = "address", EmergencyContactName = "EmergencyContactName", EmergencyContactPhone = "111" },
                new Patient() { PatientId = 2, PersonId = 2, Phone = "111", Address = "address", EmergencyContactName = "EmergencyContactName", EmergencyContactPhone = "111" }
            );
        }
    }
}

