using System.Data.Entity.ModelConfiguration;
using TestSite.Models;

namespace TestSite.Persistence.EntityConfigurations
{
    public class ArticleConfiguration : EntityTypeConfiguration<Article>
    {
        public ArticleConfiguration()
        {
            Property(c => c.ArticleId).IsRequired();
            Property(c => c.TopicId).IsRequired();
            Property(c => c.Content).IsRequired();
            Property(c => c.CreateDate).IsRequired();
            Property(c => c.Name).IsRequired();
            Property(c => c.EditDate).IsRequired();
            Property(c => c.LastEditId).IsRequired();
            Property(c => c.EditCount).IsRequired();
            Property(c => c.CreateId).IsRequired();
            Property(c => c.Delete).IsRequired();
        }
    }
}