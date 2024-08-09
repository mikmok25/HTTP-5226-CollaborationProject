namespace VendorManagement2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategoryEvent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CategoryEvents",
                c => new
                    {
                        CategoryEventID = c.Int(nullable: false, identity: true),
                        CategoryID = c.Int(nullable: false),
                        EventID = c.Int(nullable: false),
                        Status = c.String(),
                        Notes = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryEventID)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .ForeignKey("dbo.Events", t => t.EventID, cascadeDelete: true)
                .Index(t => t.CategoryID)
                .Index(t => t.EventID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CategoryEvents", "EventID", "dbo.Events");
            DropForeignKey("dbo.CategoryEvents", "CategoryID", "dbo.Categories");
            DropIndex("dbo.CategoryEvents", new[] { "EventID" });
            DropIndex("dbo.CategoryEvents", new[] { "CategoryID" });
            DropTable("dbo.CategoryEvents");
        }
    }
}
