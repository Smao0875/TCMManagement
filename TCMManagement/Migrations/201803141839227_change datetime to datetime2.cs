namespace TCMManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedatetimetodatetime2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Appointments", "TimeStart", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Appointments", "TimeEnd", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Appointments", "DateCreated", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Patients", "DateCreated", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Patients", "DateOfBirth", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.MedicalHistoryRecords", "DateCreated", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.TreatmentRecords", "DateCreated", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.People", "DateCreated", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.People", "DateCreated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TreatmentRecords", "DateCreated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.MedicalHistoryRecords", "DateCreated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Patients", "DateOfBirth", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Patients", "DateCreated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Appointments", "DateCreated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Appointments", "TimeEnd", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Appointments", "TimeStart", c => c.DateTime(nullable: false));
        }
    }
}
