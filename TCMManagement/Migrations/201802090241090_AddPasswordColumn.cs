namespace TCMManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPasswordColumn : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TblPerson", newName: "People");
            AddColumn("dbo.People", "Password", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.People", "Password");
            RenameTable(name: "dbo.People", newName: "TblPerson");
        }
    }
}
