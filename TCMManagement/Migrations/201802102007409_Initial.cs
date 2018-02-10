namespace TCMManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
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
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        PersonId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        DateOfBirth = c.String(),
                        Gender = c.String(),
                        Email = c.String(),
                        Note = c.String(),
                        Password = c.String(),
                        UserRoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PersonId)
                .ForeignKey("dbo.UserRoles", t => t.UserRoleId, cascadeDelete: true)
                .Index(t => t.UserRoleId);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Patients", "PersonId", "dbo.People");
            DropForeignKey("dbo.People", "UserRoleId", "dbo.UserRoles");
            DropIndex("dbo.People", new[] { "UserRoleId" });
            DropIndex("dbo.Patients", new[] { "PersonId" });
            DropTable("dbo.UserRoles");
            DropTable("dbo.People");
            DropTable("dbo.Patients");
        }
    }
}
