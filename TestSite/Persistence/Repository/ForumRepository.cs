using System.Data.Entity;
using TestSite.Models;
using TestSite.Persistence.Repository.IRepository;

namespace TestSite.Persistence.Repository
{
    public class ForumRepository : Repository<Forum>, IForumRepository
    {
        public ForumRepository(DbContext context) : base(context)
        {
        }
    }
}