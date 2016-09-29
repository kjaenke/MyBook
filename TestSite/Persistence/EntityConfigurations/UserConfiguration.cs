using System.Data.Entity.ModelConfiguration;
using TestSite.Models;

namespace TestSite.Persistence.EntityConfigurations
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            Property(c => c.Id)
                .IsRequired();

            Property(c => c.Lastname)
                .IsRequired();

            Property(c => c.Mail)
                .IsRequired()
                .HasMaxLength(256);
            Property(c => c.Salutation)
                .HasMaxLength(5);

            Property(c => c.Firstname)
                .HasMaxLength(256);
            Property(c => c.Phonenumber)
                .HasMaxLength(256);
            Property(c => c.Roles)
                .HasMaxLength(256);
        }
    }
}