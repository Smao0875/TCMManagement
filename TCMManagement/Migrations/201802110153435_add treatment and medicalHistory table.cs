namespace TCMManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtreatmentandmedicalHistorytable : DbMigration
    {
        public override void Up()
        {
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
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: false)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: false)
                .Index(t => t.PersonId)
                .Index(t => t.PatientId);
            
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
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: false)
                .Index(t => t.PatientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MedicalHistoryRecords", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.TreatmentRecords", "PersonId", "dbo.People");
            DropForeignKey("dbo.TreatmentRecords", "PatientId", "dbo.Patients");
            DropIndex("dbo.MedicalHistoryRecords", new[] { "PatientId" });
            DropIndex("dbo.TreatmentRecords", new[] { "PatientId" });
            DropIndex("dbo.TreatmentRecords", new[] { "PersonId" });
            DropTable("dbo.MedicalHistoryRecords");
            DropTable("dbo.TreatmentRecords");
        }
    }
}
