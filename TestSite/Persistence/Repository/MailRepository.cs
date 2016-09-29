using System.Data.Entity;
using TestSite.Models;
using TestSite.Persistence.Repository.IRepository;

namespace TestSite.Persistence.Repository
{
    public class MailRepository : Repository<Mail>, IMailRepository
    {
        public MailRepository(DbContext context) : base(context)
        {
        }
    }
}