using TestSite.Infrastructure;
using TestSite.Persistence.Repository;
using TestSite.Persistence.Repository.IRepository;

namespace TestSite.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PlutoContext _context;


        public UnitOfWork(PlutoContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Contacts = new ContactRepository(_context);
            Forums = new ForumRepository(_context);
            Topics = new TopicRepository(_context);
            Articles = new ArticleRepository(_context);
            Mails = new MailRepository(_context);
        }

        public ContactRepository Contacts { get; }
        public ForumRepository Forums { get; }
        public ArticleRepository Articles { get; }
        public TopicRepository Topics { get; }
        public MailRepository Mails { get; set; }

        public UserRepository Users { get; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}