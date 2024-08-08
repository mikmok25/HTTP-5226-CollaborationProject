namespace VendorManagement2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class users : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserEvents",
                c => new
                    {
                        UserEventID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        EventID = c.Int(nullable: false),
                        Status = c.String(),
                        Notes = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserEventID)
                .ForeignKey("dbo.Events", t => t.EventID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.EventID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserEvents", "UserID", "dbo.Users");
            DropForeignKey("dbo.UserEvents", "EventID", "dbo.Events");
            DropIndex("dbo.UserEvents", new[] { "EventID" });
            DropIndex("dbo.UserEvents", new[] { "UserID" });
            DropTable("dbo.Users");
            DropTable("dbo.UserEvents");
        }
    }
}
