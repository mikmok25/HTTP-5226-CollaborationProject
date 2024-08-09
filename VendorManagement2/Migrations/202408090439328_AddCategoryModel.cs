namespace VendorManagement2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategoryModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            AddColumn("dbo.Events", "CategoryID", c => c.Int(nullable: true));
            CreateIndex("dbo.Events", "CategoryID");
            AddForeignKey("dbo.Events", "CategoryID", "dbo.Categories", "CategoryID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Events", new[] { "CategoryID" });
            DropColumn("dbo.Events", "CategoryID");
            DropTable("dbo.Categories");
        }
    }
}
