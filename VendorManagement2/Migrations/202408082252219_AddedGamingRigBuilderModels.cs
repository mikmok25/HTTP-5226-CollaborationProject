namespace VendorManagement2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedGamingRigBuilderModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BuildComponents",
                c => new
                    {
                        BuildComponentID = c.Int(nullable: false, identity: true),
                        BuildID = c.Int(nullable: false),
                        ComponentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BuildComponentID)
                .ForeignKey("dbo.Builds", t => t.BuildID, cascadeDelete: true)
                .ForeignKey("dbo.Components", t => t.ComponentID, cascadeDelete: true)
                .Index(t => t.BuildID)
                .Index(t => t.ComponentID);
            
            CreateTable(
                "dbo.Builds",
                c => new
                    {
                        BuildID = c.Int(nullable: false, identity: true),
                        BuildName = c.String(),
                        BuildDescription = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BuildID);
            
            CreateTable(
                "dbo.Components",
                c => new
                    {
                        ComponentID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Manufacturer = c.String(),
                        ImagePath = c.String(),
                    })
                .PrimaryKey(t => t.ComponentID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BuildComponents", "ComponentID", "dbo.Components");
            DropForeignKey("dbo.BuildComponents", "BuildID", "dbo.Builds");
            DropIndex("dbo.BuildComponents", new[] { "ComponentID" });
            DropIndex("dbo.BuildComponents", new[] { "BuildID" });
            DropTable("dbo.Components");
            DropTable("dbo.Builds");
            DropTable("dbo.BuildComponents");
        }
    }
}
