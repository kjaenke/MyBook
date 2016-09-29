using System.Data.Entity.ModelConfiguration;
using TestSite.Models;

namespace TestSite.Persistence.EntityConfigurations
{
    public class TopicConfiguration : EntityTypeConfiguration<Topic>
    {
        public TopicConfiguration()
        {
            Property(c => c.TopicId).IsRequired();
            Property(c => c.ForumId).IsRequired();
            Property(c => c.Content).IsRequired();
            Property(c => c.CreateDate).IsRequired();
            Property(c => c.Name).IsRequired();
            Property(c => c.EditDate).IsRequired();
            Property(c => c.LastEditId).IsRequired();
            Property(c => c.EditCount).IsRequired();
            Property(c => c.CreateId).IsRequired();
            Property(c => c.Delete).IsRequired();
            Property(c => c.Blocked).IsRequired();
            Property(c => c.Shorttag).IsRequired();
        }
    }
}