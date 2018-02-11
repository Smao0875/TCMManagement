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
                new Person() { PersonId = 1, UserRoleId = 2, FirstName = "Practitioner", LastName = "1", DateCreated = DateTime.Now },
                new Person() { PersonId = 2, UserRoleId = 2, FirstName = "Practitioner", LastName = "2", DateCreated = DateTime.Now },
                new Person() { PersonId = 3, UserRoleId = 3, FirstName = "Receptionist", LastName = "1", DateCreated = DateTime.Now },
                new Person() { PersonId = 4, UserRoleId = 4, FirstName = "Admin", LastName = "1" , Email = "email", DateCreated = DateTime.Now }
            );

            context.Patients.AddOrUpdate(x => x.PatientId,
                new Patient() { PatientId = 1, Phone = "111", UserRoleId = 1, FirstName = "Patient01", LastName = "1",
                                    Address = "address", DateCreated = DateTime.Now, DateOfBirth = DateTime.Now},
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
                }
            );

            context.Appointments.AddOrUpdate(
                new Appointment() { AppointmentId = 1, PersonId = 1, PatientId = 1, Description = "Headache", DateCreated = DateTime.Now, TimeStart = DateTime.Now, TimeEnd = DateTime.Now },
                new Appointment() { AppointmentId = 2, PersonId = 1, PatientId = 1, Description = "Foot pain", DateCreated = DateTime.Now, TimeStart = DateTime.Now, TimeEnd = DateTime.Now },
                new Appointment() { AppointmentId = 3, PersonId = 2, PatientId = 2, Description = "Fire", DateCreated = DateTime.Now, TimeStart = DateTime.Now, TimeEnd = DateTime.Now },
                new Appointment() { AppointmentId = 4, PersonId = 2, PatientId = 2, Description = "Water", DateCreated = DateTime.Now, TimeStart = DateTime.Now, TimeEnd = DateTime.Now }
            );

            context.TreatmentRecords.AddOrUpdate(
                new TreatmentRecord() { TreatmentRecordId = 1, PersonId = 1, PatientId = 1, Diagnosis = "Too much fire", DateCreated = DateTime.Now},
                new TreatmentRecord() { TreatmentRecordId = 1, PersonId = 2, PatientId = 2, Diagnosis = "Too much wood", DateCreated = DateTime.Now }
            );

            context.MedicalHistoryRecords.AddOrUpdate(
                new MedicalHistoryRecord() { MedicalHistoryRecordId = 1, PatientId = 1, Description = "Too much fire", DateCreated = DateTime.Now},
                new MedicalHistoryRecord() { MedicalHistoryRecordId = 2, PatientId = 2, Description = "Too much water", DateCreated = DateTime.Now },
                new MedicalHistoryRecord() { MedicalHistoryRecordId = 3, PatientId = 1, Description = "Too much wood" , DateCreated = DateTime.Now },
                new MedicalHistoryRecord() { MedicalHistoryRecordId = 4, PatientId = 2, Description = "Too much metal" , DateCreated = DateTime.Now },
                new MedicalHistoryRecord() { MedicalHistoryRecordId = 5, PatientId = 1, Description = "Too much soil" , DateCreated = DateTime.Now },
                new MedicalHistoryRecord() { MedicalHistoryRecordId = 6, PatientId = 2, Description = "Too much nothing", DateCreated = DateTime.Now }
            );
        }
    }
}

