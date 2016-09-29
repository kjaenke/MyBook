using System.Data.Entity;
using TestSite.Models;
using TestSite.Persistence.Repository.IRepository;

namespace TestSite.Persistence.Repository
{
    public class TopicRepository : Repository<Topic>, ITopicRepository
    {
        public TopicRepository(DbContext context) : base(context)
        {
        }
    }
}