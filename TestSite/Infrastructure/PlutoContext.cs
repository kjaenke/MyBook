using System.Data.Entity;
using TestSite.Models;

namespace TestSite.Infrastructure
{
    public sealed class PlutoContext : DbContext
    {
        public PlutoContext()
            : base("Server=localhost;Initial Catalog=ContactList;Integrated Security=True;Min Pool Size=10;Pooling=True"
                )
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Forum> Forums { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Mail> Mails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}