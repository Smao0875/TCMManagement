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
                new UserRole() { UserRoleId = 4, Role = 4, Description = "Admin"}
            );

            context.People.AddOrUpdate(x => x.PersonId,
                new Person() { PersonId = 1, UserRoleId = 2, FirstName = "John", LastName = "Doe", Phone = "4166666666", Email = "practitioner1@tcm.com", Password = "practitioner", DateCreated = DateTime.Now, Gender = "male", Note="Special in Medicine"},
                new Person() { PersonId = 2, UserRoleId = 2, FirstName = "Zhang", LastName = "San", Phone = "4166666666", Email = "practitioner2@tcm.com", Password = "practitioner", DateCreated = DateTime.Now, Gender = "female", Note = "Special in Massage" },
                new Person() { PersonId = 3, UserRoleId = 2, FirstName = "Bob", LastName = "Dylan", Phone = "4166666666", Email = "practitioner3@tcm.com", Password = "practitioner", DateCreated = DateTime.Now, Gender = "male", Note = "Special in Acupuncture" },
                new Person() { PersonId = 4, UserRoleId = 3, FirstName = "Li", LastName = "Si", Phone = "4166666666", Email = "receptionist1@tcm.com", Password = "receptionist", DateCreated = DateTime.Now, Gender = "female"},
                new Person() { PersonId = 5, UserRoleId = 3, FirstName = "Van", LastName = "Gogh", Phone = "4166666666", Email = "receptionist2@tcm.com", Password = "receptionist", DateCreated = DateTime.Now, Gender = "male", Note = "Artist" },
                new Person() { PersonId = 6, UserRoleId = 4, FirstName = "Wang", LastName = "Wu" , Email = "admin@tcm.com", Password = "admin", DateCreated = DateTime.Now }
            );

            context.Patients.AddOrUpdate(x => x.PatientId,
                new Patient() { PatientId = 1, Phone = "5193339999", UserRoleId = 1, FirstName = "Big", LastName = "Mike", Gender = "male", Email = "patient1@tcm.com", Address = "150 King ST Toronto", DateCreated = DateTime.Now, DateOfBirth = DateTime.Now.AddYears(-20)},
                new Patient()
                {
                    PatientId = 2,
                    Phone = "4166666666",
                    UserRoleId = 1,
                    FirstName = "One",
                    LastName = "Patient",
                    Gender = "male",
                    Email = "patient2@tcm.com",
                    Address = "200 King ST Waterloo",
                    DateCreated = DateTime.Now,
                    DateOfBirth = DateTime.Now.AddYears(-22)
                },
                new Patient()
                {
                    PatientId = 3,
                    Phone = "9053335555",
                    UserRoleId = 1,
                    FirstName = "Two",
                    LastName = "Patient",
                    Gender = "male",
                    Email = "patient3@tcm.com",
                    Address = "100 Unknown ST Kitchener",
                    DateCreated = DateTime.Now,
                    DateOfBirth = DateTime.Now.AddYears(-50)
                },
                new Patient()
                {
                    PatientId = 4,
                    Phone = "9053335555",
                    UserRoleId = 1,
                    FirstName = "Three",
                    LastName = "Patient",
                    Gender = "female",
                    Email = "patient4@tcm.com",
                    Address = "100 Bay ST Toronto",
                    DateCreated = DateTime.Now,
                    DateOfBirth = DateTime.Now.AddYears(-32)
                },
                new Patient()
                {
                    PatientId = 5,
                    Phone = "9053335555",
                    UserRoleId = 1,
                    FirstName = "Four",
                    LastName = "Patient",
                    Gender = "male",
                    Email = "patient5@tcm.com",
                    Address = "1000 Queen ST Toronto",
                    DateCreated = DateTime.Now,
                    DateOfBirth = DateTime.Now.AddYears(-30)
                }, 
                new Patient()
                {
                    PatientId = 6,
                    Phone = "9053335555",
                    UserRoleId = 1,
                    FirstName = "Five",
                    LastName = "Patient",
                    Gender = "female",
                    Email = "patient6@tcm.com",
                    Address = "1000 Adelade ST Toronto",
                    DateCreated = DateTime.Now,
                    DateOfBirth = DateTime.Now.AddYears(-30)
                }

            );

            context.Appointments.AddOrUpdate(
                new Appointment() { PersonId = 1, PatientId = 1, Description = "Headache", DateCreated = DateTime.Now, TimeStart = DateTime.Now, TimeEnd = DateTime.Now.AddHours(0.5) },
                new Appointment() { PersonId = 1, PatientId = 2, Description = "Chronical pain on lower back", DateCreated = DateTime.Now, TimeStart = DateTime.Now.AddHours(2), TimeEnd = DateTime.Now.AddHours(2.5) },
                new Appointment() { PersonId = 1, PatientId = 3, Description = "Mouth dry", DateCreated = DateTime.Now, TimeStart = DateTime.Now.AddHours(10), TimeEnd = DateTime.Now.AddHours(10.5) },
                new Appointment() { PersonId = 2, PatientId = 3, Description = "Hearing loss", DateCreated = DateTime.Now, TimeStart = DateTime.Now.AddHours(14), TimeEnd = DateTime.Now.AddHours(14.5) },
                new Appointment() { PersonId = 2, PatientId = 4, Description = "Couldn't fall asleep", DateCreated = DateTime.Now, TimeStart = DateTime.Now, TimeEnd = DateTime.Now.AddHours(0.5) },
                new Appointment() { PersonId = 2, PatientId = 5, Description = "Digestion problem", DateCreated = DateTime.Now, TimeStart = DateTime.Now.AddHours(2), TimeEnd = DateTime.Now.AddHours(2.5) },
                new Appointment() { PersonId = 3, PatientId = 1, Description = "Hurt back", DateCreated = DateTime.Now, TimeStart = DateTime.Now.AddHours(2), TimeEnd = DateTime.Now.AddHours(2.5) },
                new Appointment() { PersonId = 3, PatientId = 2, Description = "Decreased vision", DateCreated = DateTime.Now, TimeStart = DateTime.Now.AddHours(10), TimeEnd = DateTime.Now.AddHours(10.5) },
                new Appointment() { PersonId = 3, PatientId = 3, Description = "High blood pressure", DateCreated = DateTime.Now, TimeStart = DateTime.Now.AddHours(14), TimeEnd = DateTime.Now.AddHours(14.5) },
                new Appointment() { PersonId = 3, PatientId = 4, Description = "High blood suger", DateCreated = DateTime.Now, TimeStart = DateTime.Now.AddHours(18), TimeEnd = DateTime.Now.AddHours(18.5) }
            );

            context.TreatmentRecords.AddOrUpdate(
                new TreatmentRecord() { TreatmentRecordId = 1, PersonId = 1, PatientId = 1, Symptom = "Chronical pain on lower back", Diagnosis = "Acupuncture", PaymentMethod = "Cash", TotalAmount = 100, AmountPaid = 100, DateCreated = DateTime.Now},
                new TreatmentRecord() { TreatmentRecordId = 1, PersonId = 1, PatientId = 1, Symptom = "High blood pressure", Diagnosis = "Point massage", PaymentMethod = "Credit Card", TotalAmount = 150, AmountPaid = 150, DateCreated = DateTime.Now }
            );

            context.MedicalHistoryRecords.AddOrUpdate(
                new MedicalHistoryRecord() { MedicalHistoryRecordId = 1, PatientId = 1, Description = "Chronical pain on lower back", DateCreated = DateTime.Now, IsFamily = false},
                new MedicalHistoryRecord() { MedicalHistoryRecordId = 2, PatientId = 1, Description = "Digestion problem", DateCreated = DateTime.Now , IsFamily = false}
            );
        }
    }
}

