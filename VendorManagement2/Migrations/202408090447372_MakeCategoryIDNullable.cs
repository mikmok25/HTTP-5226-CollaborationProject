namespace VendorManagement2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeCategoryIDNullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Events", new[] { "CategoryID" });
            AlterColumn("dbo.Events", "CategoryID", c => c.Int());
            CreateIndex("dbo.Events", "CategoryID");
            AddForeignKey("dbo.Events", "CategoryID", "dbo.Categories", "CategoryID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Events", new[] { "CategoryID" });
            AlterColumn("dbo.Events", "CategoryID", c => c.Int(nullable: false));
            CreateIndex("dbo.Events", "CategoryID");
            AddForeignKey("dbo.Events", "CategoryID", "dbo.Categories", "CategoryID", cascadeDelete: true);
        }
    }
}
