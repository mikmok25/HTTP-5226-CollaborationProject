using System.Data.Entity.Migrations;

public partial class AddUserEventForeignKey : DbMigration
{
    public override void Up()
    {
        AddColumn("dbo.UserEvents", "UserID", c => c.String(nullable: false, maxLength: 128));
        CreateIndex("dbo.UserEvents", "UserID");
        AddForeignKey("dbo.UserEvents", "UserID", "dbo.Users", "Id", cascadeDelete: true);
    }

    public override void Down()
    {
        DropForeignKey("dbo.UserEvents", "UserID", "dbo.Users");
        DropIndex("dbo.UserEvents", new[] { "UserID" });
        DropColumn("dbo.UserEvents", "UserID");
    }
}
