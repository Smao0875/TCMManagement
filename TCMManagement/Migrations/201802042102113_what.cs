namespace TCMManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class what : DbMigration
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
                        PatientId = c.Int(nullable: false),
                        PractitionerId = c.Int(nullable: false),
                        Patient_PersonId = c.Int(),
                        Practitioner_PersonId = c.Int(),
                        Person_PersonId = c.Int(),
                    })
                .PrimaryKey(t => t.AppointmentId)
                .ForeignKey("dbo.TblPerson", t => t.Patient_PersonId)
                .ForeignKey("dbo.TblPerson", t => t.Practitioner_PersonId)
                .ForeignKey("dbo.TblPerson", t => t.Person_PersonId)
                .Index(t => t.Patient_PersonId)
                .Index(t => t.Practitioner_PersonId)
                .Index(t => t.Person_PersonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "Person_PersonId", "dbo.TblPerson");
            DropForeignKey("dbo.Appointments", "Practitioner_PersonId", "dbo.TblPerson");
            DropForeignKey("dbo.Appointments", "Patient_PersonId", "dbo.TblPerson");
            DropIndex("dbo.Appointments", new[] { "Person_PersonId" });
            DropIndex("dbo.Appointments", new[] { "Practitioner_PersonId" });
            DropIndex("dbo.Appointments", new[] { "Patient_PersonId" });
            DropTable("dbo.Appointments");
        }
    }
}
