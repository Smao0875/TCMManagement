namespace TCMManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using TCMManagement.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TcmContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "TCMManagement.Models.TcmContext";
        }

        protected override void Seed(TcmContext context)
        {
            context.Roles.AddOrUpdate(x => x.UserRoleId,
                new UserRole() { UserRoleId = 1, Role = 1, Description = "Patient"},
                new UserRole() { UserRoleId = 2, Role = 2, Description = "Practitioner"},
                new UserRole() { UserRoleId = 3, Role = 3, Description = "Receptionist"},
                new UserRole() { UserRoleId = 4, Role = 4, Description = "admin"}
            );

            context.People.AddOrUpdate(x => x.PersonId,
                new Person() { PersonId = 1, UserRoleId = 2, FirstName = "John", LastName = "Doe", Email = "p", Password = "p", DateCreated = DateTime.Now },
                new Person() { PersonId = 2, UserRoleId = 2, FirstName = "Zhang", LastName = "San", Email = "p", Password = "p", DateCreated = DateTime.Now },
                new Person() { PersonId = 3, UserRoleId = 3, FirstName = "Li", LastName = "Si", Email = "r", Password = "r", DateCreated = DateTime.Now },
                new Person() { PersonId = 4, UserRoleId = 4, FirstName = "Wang", LastName = "Wu" , Email = "admin", Password = "admin", DateCreated = DateTime.Now }
            );

            context.Patients.AddOrUpdate(x => x.PatientId,
                new Patient() { PatientId = 1, Phone = "5199999999", UserRoleId = 1, FirstName = "Patient01", LastName = "1", Address = "address", DateCreated = DateTime.Now, DateOfBirth = DateTime.Now},
                new Patient()
                {
                    PatientId = 2,
                    Phone = "111",
                    UserRoleId = 1,
                    FirstName = "Patient02",
                    LastName = "1",
                    Address = "address",
                    DateCreated = DateTime.Now,
                    DateOfBirth = DateTime.Now
                },
                new Patient()
                {
                    PatientId = 3,
                    Phone = "111",
                    UserRoleId = 1,
                    FirstName = "Patient03",
                    LastName = "1",
                    Address = "address",
                    DateCreated = DateTime.Now,
                    DateOfBirth = DateTime.Now
                },
                new Patient()
                {
                    PatientId = 4,
                    Phone = "111",
                    UserRoleId = 1,
                    FirstName = "Patient03",
                    LastName = "1",
                    Address = "address",
                    DateCreated = DateTime.Now,
                    DateOfBirth = DateTime.Now
                },
                new Patient()
                {
                    PatientId = 5,
                    Phone = "111",
                    UserRoleId = 1,
                    FirstName = "Patient03",
                    LastName = "1",
                    Address = "address",
                    DateCreated = DateTime.Now,
                    DateOfBirth = DateTime.Now
                }
            );

            context.Appointments.AddOrUpdate(
                new Appointment() { PersonId = 1, PatientId = 1, Description = "Headache", DateCreated = DateTime.Now, TimeStart = DateTime.Now, TimeEnd = DateTime.Now.AddHours(1) },
                new Appointment() { PersonId = 1, PatientId = 1, Description = "Foot pain", DateCreated = DateTime.Now, TimeStart = DateTime.Now.AddHours(2), TimeEnd = DateTime.Now.AddHours(4) },
                new Appointment() { PersonId = 1, PatientId = 2, Description = "Fire", DateCreated = DateTime.Now, TimeStart = DateTime.Now.AddHours(10), TimeEnd = DateTime.Now.AddHours(12) },
                new Appointment() { PersonId = 1, PatientId = 2, Description = "Water", DateCreated = DateTime.Now, TimeStart = DateTime.Now.AddHours(14), TimeEnd = DateTime.Now.AddHours(17) },
                new Appointment() { PersonId = 2, PatientId = 1, Description = "Practitioner2", DateCreated = DateTime.Now, TimeStart = DateTime.Now, TimeEnd = DateTime.Now.AddHours(1) },
                new Appointment() { PersonId = 2, PatientId = 1, Description = "Practitioner2", DateCreated = DateTime.Now, TimeStart = DateTime.Now.AddHours(2), TimeEnd = DateTime.Now.AddHours(4) },
                new Appointment() { PersonId = 2, PatientId = 2, Description = "Practitioner2", DateCreated = DateTime.Now, TimeStart = DateTime.Now.AddHours(10), TimeEnd = DateTime.Now.AddHours(12) },
                new Appointment() { PersonId = 2, PatientId = 2, Description = "Practitioner2", DateCreated = DateTime.Now, TimeStart = DateTime.Now.AddHours(14), TimeEnd = DateTime.Now.AddHours(17) }
            );

            context.TreatmentRecords.AddOrUpdate(
                new TreatmentRecord() { TreatmentRecordId = 1, PersonId = 1, PatientId = 1, Diagnosis = "Too much fire", DateCreated = DateTime.Now},
                new TreatmentRecord() { TreatmentRecordId = 1, PersonId = 2, PatientId = 2, Diagnosis = "Too much wood", DateCreated = DateTime.Now }
            );

            context.MedicalHistoryRecords.AddOrUpdate(
                new MedicalHistoryRecord() { MedicalHistoryRecordId = 1, PatientId = 1, Description = "Too much fire", DateCreated = DateTime.Now, IsFamily = false},
                new MedicalHistoryRecord() { MedicalHistoryRecordId = 2, PatientId = 2, Description = "Too much water", DateCreated = DateTime.Now , IsFamily = false},
                new MedicalHistoryRecord() { MedicalHistoryRecordId = 3, PatientId = 1, Description = "Too much wood" , DateCreated = DateTime.Now , IsFamily = false},
                new MedicalHistoryRecord() { MedicalHistoryRecordId = 4, PatientId = 2, Description = "Too much metal" , DateCreated = DateTime.Now , IsFamily = false},
                new MedicalHistoryRecord() { MedicalHistoryRecordId = 5, PatientId = 1, Description = "Too much soil" , DateCreated = DateTime.Now, IsFamily = false},
                new MedicalHistoryRecord() { MedicalHistoryRecordId = 6, PatientId = 2, Description = "Too much nothing", DateCreated = DateTime.Now, IsFamily = false},
                new MedicalHistoryRecord() { MedicalHistoryRecordId = 7, PatientId = 1, Description = "Too much soil" , DateCreated = DateTime.Now, IsFamily = true},
                new MedicalHistoryRecord() { MedicalHistoryRecordId = 8, PatientId = 2, Description = "Too much nothing", DateCreated = DateTime.Now, IsFamily = true}
            );
        }
    }
}

