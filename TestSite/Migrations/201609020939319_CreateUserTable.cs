using System.Data.Entity.Migrations;

namespace TestSite.Migrations
{
    public partial class CreateUserTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                {
                    Id = c.Int(false, true),
                    Salutation = c.String(),
                    Lastname = c.String(false),
                    Firstname = c.String(),
                    Mail = c.String(false),
                    Phonenumber = c.String(),
                    Password = c.String(false),
                    Roles = c.String()
                })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}