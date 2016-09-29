using System.Data.Entity.ModelConfiguration;
using TestSite.Models;

namespace TestSite.Persistence.EntityConfigurations
{
    public class ContactConfiguration : EntityTypeConfiguration<Contact>
    {
        public ContactConfiguration()
        {
            Property(c => c.Id);
            Property(c => c.Firstname).IsRequired().HasMaxLength(256);
            Property(c => c.Lastname).IsRequired().HasMaxLength(256);
            Property(c => c.StreetHouseNumber).HasMaxLength(256);
            Property(c => c.PlzAndOrt).IsRequired().HasMaxLength(256);
            Property(c => c.TelephonNumber).HasMaxLength(256);
            Property(c => c.EMail).IsRequired().HasMaxLength(256);
            Property(c => c.Homepage).HasMaxLength(256);
            Property(c => c.Status).IsRequired();
            Property(c => c.CreateById).IsRequired();
        }
    }
}