namespace TCMManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Seperatepatientsfrompersontable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Appointments", "Person_PersonId", "dbo.TblPerson");
            DropForeignKey("dbo.Appointments", "Patient_PersonId", "dbo.TblPerson");
            DropForeignKey("dbo.Appointments", "Practitioner_PersonId", "dbo.TblPerson");
            DropIndex("dbo.Appointments", new[] { "Patient_PersonId" });
            DropIndex("dbo.Appointments", new[] { "Practitioner_PersonId" });
            DropIndex("dbo.Appointments", new[] { "Person_PersonId" });
            DropColumn("dbo.Appointments", "PatientId");
            RenameColumn(table: "dbo.Appointments", name: "Patient_PersonId", newName: "PatientId");
            RenameColumn(table: "dbo.Appointments", name: "Practitioner_PersonId", newName: "PersonId");
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        PatientId = c.Int(nullable: false, identity: true),
                        Phone = c.String(),
                        Address = c.String(),
                        EmergencyContactName = c.String(),
                        EmergencyContactPhone = c.String(),
                        PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PatientId)
                .ForeignKey("dbo.TblPerson", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId);
            
            AlterColumn("dbo.Appointments", "PatientId", c => c.Int(nullable: false));
            AlterColumn("dbo.Appointments", "PersonId", c => c.Int(nullable: false));
            CreateIndex("dbo.Appointments", "PatientId");
            CreateIndex("dbo.Appointments", "PersonId");
            AddForeignKey("dbo.Appointments", "PersonId", "dbo.TblPerson", "PersonId", cascadeDelete: false);
            AddForeignKey("dbo.Appointments", "PatientId", "dbo.Patients", "PatientId", cascadeDelete: false);
            DropColumn("dbo.TblPerson", "Phone");
            DropColumn("dbo.TblPerson", "Address");
            DropColumn("dbo.Appointments", "PractitionerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Appointments", "PractitionerId", c => c.Int(nullable: false));
            AddColumn("dbo.TblPerson", "Address", c => c.String());
            AddColumn("dbo.TblPerson", "Phone", c => c.String());
            DropForeignKey("dbo.Appointments", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.Appointments", "PersonId", "dbo.TblPerson");
            DropForeignKey("dbo.Patients", "PersonId", "dbo.TblPerson");
            DropIndex("dbo.Patients", new[] { "PersonId" });
            DropIndex("dbo.Appointments", new[] { "PersonId" });
            DropIndex("dbo.Appointments", new[] { "PatientId" });
            AlterColumn("dbo.Appointments", "PersonId", c => c.Int());
            AlterColumn("dbo.Appointments", "PersonId", c => c.Int());
            AlterColumn("dbo.Appointments", "PatientId", c => c.Int());
            DropTable("dbo.Patients");
            RenameColumn(table: "dbo.Appointments", name: "PersonId", newName: "Practitioner_PersonId");
            RenameColumn(table: "dbo.Appointments", name: "PatientId", newName: "Patient_PersonId");
            RenameColumn(table: "dbo.Appointments", name: "PersonId", newName: "Person_PersonId");
            AddColumn("dbo.Appointments", "PatientId", c => c.Int(nullable: false));
            CreateIndex("dbo.Appointments", "Person_PersonId");
            CreateIndex("dbo.Appointments", "Practitioner_PersonId");
            CreateIndex("dbo.Appointments", "Patient_PersonId");
            AddForeignKey("dbo.Appointments", "Practitioner_PersonId", "dbo.TblPerson", "PersonId");
            AddForeignKey("dbo.Appointments", "Patient_PersonId", "dbo.TblPerson", "PersonId");
            AddForeignKey("dbo.Appointments", "Person_PersonId", "dbo.TblPerson", "PersonId");
        }
    }
}
