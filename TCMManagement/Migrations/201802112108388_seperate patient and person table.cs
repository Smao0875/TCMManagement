namespace TCMManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seperatepatientandpersontable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        AppointmentId = c.Int(nullable: false, identity: true),
                        TimeStart = c.DateTime(nullable: false),
                        TimeEnd = c.DateTime(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        Description = c.String(),
                        PersonId = c.Int(nullable: false),
                        PatientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AppointmentId)
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        PatientId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Gender = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        Phone = c.String(),
                        Note = c.String(),
                        Password = c.String(),
                        Address = c.String(),
                        EmergencyContactName = c.String(),
                        EmergencyContactPhone = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        UserRoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PatientId)
                .ForeignKey("dbo.UserRoles", t => t.UserRoleId, cascadeDelete: false)
                .Index(t => t.UserRoleId);
            
            CreateTable(
                "dbo.MedicalHistoryRecords",
                c => new
                    {
                        MedicalHistoryRecordId = c.Int(nullable: false, identity: true),
                        DateCreated = c.DateTime(nullable: false),
                        Description = c.String(),
                        Medication = c.String(),
                        Duration = c.String(),
                        Dosage = c.String(),
                        IsFamily = c.Boolean(nullable: false),
                        PatientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MedicalHistoryRecordId)
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserRoleId = c.Int(nullable: false, identity: true),
                        Role = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.UserRoleId);
            
            CreateTable(
                "dbo.TreatmentRecords",
                c => new
                    {
                        TreatmentRecordId = c.Int(nullable: false, identity: true),
                        DateCreated = c.DateTime(nullable: false),
                        Symptom = c.String(),
                        Diagnosis = c.String(),
                        PrescriptionID = c.Int(nullable: false),
                        PersonId = c.Int(nullable: false),
                        PatientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TreatmentRecordId)
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        PersonId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Gender = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        Phone = c.String(),
                        Note = c.String(),
                        Password = c.String(),
                        UserRoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PersonId)
                .ForeignKey("dbo.UserRoles", t => t.UserRoleId, cascadeDelete: false)
                .Index(t => t.UserRoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TreatmentRecords", "PersonId", "dbo.People");
            DropForeignKey("dbo.People", "UserRoleId", "dbo.UserRoles");
            DropForeignKey("dbo.Appointments", "PersonId", "dbo.People");
            DropForeignKey("dbo.TreatmentRecords", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.Patients", "UserRoleId", "dbo.UserRoles");
            DropForeignKey("dbo.MedicalHistoryRecords", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.Appointments", "PatientId", "dbo.Patients");
            DropIndex("dbo.People", new[] { "UserRoleId" });
            DropIndex("dbo.TreatmentRecords", new[] { "PatientId" });
            DropIndex("dbo.TreatmentRecords", new[] { "PersonId" });
            DropIndex("dbo.MedicalHistoryRecords", new[] { "PatientId" });
            DropIndex("dbo.Patients", new[] { "UserRoleId" });
            DropIndex("dbo.Appointments", new[] { "PatientId" });
            DropIndex("dbo.Appointments", new[] { "PersonId" });
            DropTable("dbo.People");
            DropTable("dbo.TreatmentRecords");
            DropTable("dbo.UserRoles");
            DropTable("dbo.MedicalHistoryRecords");
            DropTable("dbo.Patients");
            DropTable("dbo.Appointments");
        }
    }
}
