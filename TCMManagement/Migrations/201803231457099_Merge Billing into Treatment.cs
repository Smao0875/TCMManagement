namespace TCMManagement.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class MergeBillingintoTreatment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TreatmentRecords", "TotalAmount", c => c.Double(nullable: false, defaultValue:0));
            AddColumn("dbo.TreatmentRecords", "Change", c => c.Double(nullable: false, defaultValue: 0));
            AddColumn("dbo.TreatmentRecords", "AmountPaid", c => c.Double(nullable: false, defaultValue: 0));
            AddColumn("dbo.TreatmentRecords", "PaymentMethod", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TreatmentRecords", "PaymentMethod");
            DropColumn("dbo.TreatmentRecords", "AmountPaid");
            DropColumn("dbo.TreatmentRecords", "Change");
            DropColumn("dbo.TreatmentRecords", "TotalAmount");
        }
    }
}
