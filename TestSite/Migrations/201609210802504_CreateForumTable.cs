using System.Data.Entity.Migrations;

namespace TestSite.Migrations
{
    public partial class CreateForumTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fora",
                c => new
                {
                    ForumId = c.Int (nullable: false, identity:true),
                    ForumName = c.String(),
                    Status = c.String(),
                    Erstelldatum = c.String(),
                    Comment = c.String(),
                    Delete = c.String()
                })
                .PrimaryKey(t => t.ForumId);
        }

        public override void Down()
        {
            DropTable("dbo.Fora");
        }
    }
}