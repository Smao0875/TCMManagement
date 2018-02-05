namespace TCMManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.TblPerson");
            DropColumn("dbo.TblPerson", "Id");
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserRoleId = c.Int(nullable: false, identity: true),
                        Role = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.UserRoleId);
            
            AddColumn("dbo.TblPerson", "PersonId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.TblPerson", "UserRoleId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.TblPerson", "PersonId");
            CreateIndex("dbo.TblPerson", "UserRoleId");
            AddForeignKey("dbo.TblPerson", "UserRoleId", "dbo.UserRoles", "UserRoleId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            AddColumn("dbo.TblPerson", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.TblPerson", "UserRoleId", "dbo.UserRoles");
            DropIndex("dbo.TblPerson", new[] { "UserRoleId" });
            DropPrimaryKey("dbo.TblPerson");
            DropColumn("dbo.TblPerson", "UserRoleId");
            DropColumn("dbo.TblPerson", "PersonId");
            DropTable("dbo.UserRoles");
            AddPrimaryKey("dbo.TblPerson", "Id");
        }
    }
}
