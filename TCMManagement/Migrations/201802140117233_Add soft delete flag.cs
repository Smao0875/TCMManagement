namespace TCMManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addsoftdeleteflag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patients", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.MedicalHistoryRecords", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.TreatmentRecords", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.People", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.People", "IsDeleted");
            DropColumn("dbo.TreatmentRecords", "IsDeleted");
            DropColumn("dbo.MedicalHistoryRecords", "IsDeleted");
            DropColumn("dbo.Patients", "IsDeleted");
        }
    }
}
