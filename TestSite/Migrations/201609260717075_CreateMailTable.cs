using System.Data.Entity.Migrations;

namespace TestSite.Migrations
{
    public partial class CreateMailTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mails", "ToId", c => c.String());
            AddColumn("dbo.Mails", "IsRead", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.Mails", "IsRead");
            DropColumn("dbo.Mails", "ToId");
        }
    }
}