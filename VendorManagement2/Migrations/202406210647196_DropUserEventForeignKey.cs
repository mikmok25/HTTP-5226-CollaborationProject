using System.Data.Entity.Migrations;

public partial class DropUserEventForeignKey : DbMigration
{
    public override void Up()
    {
        DropForeignKey("dbo.UserEvents", "UserID", "dbo.Users");
        DropIndex("dbo.UserEvents", new[] { "UserID" });
        DropColumn("dbo.UserEvents", "UserID");
    }

    public override void Down()
    {
        AddColumn("dbo.UserEvents", "UserID", c => c.String(maxLength: 128));
        CreateIndex("dbo.UserEvents", "UserID");
        AddForeignKey("dbo.UserEvents", "UserID", "dbo.Users", "Id");
    }
}
