using System.Data.Entity.Migrations;

namespace TestSite.Migrations
{
    public partial class CreateContactTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                {
                    Id = c.Int(true, true),
                    Firstname = c.String(),
                    Lastname = c.String(),
                    StreetHouseNumber = c.String(),
                    PlzAndOrt = c.String(),
                    TelephonNumber = c.String(),
                    EMail = c.String(),
                    Homepage = c.String(),
                    status = c.Int(false),
                    CreateById = c.Int(false)
                })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropTable("dbo.Contacts");
        }
    }
}