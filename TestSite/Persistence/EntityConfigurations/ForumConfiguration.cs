using System.Data.Entity.ModelConfiguration;
using TestSite.Models;

namespace TestSite.Persistence.EntityConfigurations
{
    public class ForumConfiguration : EntityTypeConfiguration<Forum>
    {
        public ForumConfiguration()
        {
            Property(c => c.ForumId);
             
            Property(c => c.ForumName)
                .IsRequired();
            Property(c => c.Status)
                .IsRequired();
            Property(c => c.Erstelldatum)
                .IsRequired();
            Property(c => c.Comment)
                .IsRequired();
            Property(c => c.Delete);

        }
    }
}