using System.Data.Entity;
using System.Linq;
using System.Web.WebPages;
using TestSite.Infrastructure;
using TestSite.Models;
using TestSite.Persistence.Repository.IRepository;

namespace TestSite.Persistence.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context)
            : base(context)
        {
        }


        public User FindMail(string mailadress)
        {
            mailadress = mailadress.ToLowerInvariant();

            return Context.Set<User>().FirstOrDefault(m => m.Mail == mailadress);
        }

        public void RemoveUser(string mailadress)
        {
            User user = Context.Set<User>().FirstOrDefault(m => m.Mail == mailadress);
            if (user == null)
            {
                return;
            }
            Context.Set<User>().Remove(user);
        }

        public void UserRank(string id, string funktion)
        {
            UnitOfWork unit = new UnitOfWork(new PlutoContext());
            User user = unit.Users.Get(id.AsInt());

            unit.Users.Add(Rank(user, funktion));
            unit.Complete();
        }

        public User GetUser(string mailadress, string password)
        {
            var usertest = Context.Set<User>().FirstOrDefault(m => m.Mail == mailadress);
            if (usertest != null)
            {
                if (usertest.Mail != null && usertest.Password != null)
                {
                    if (usertest.Mail == mailadress && usertest.Password == password)
                    {
                        return usertest;
                    }
                    return null;
                }
                return null;
            }

            {
                return null;
            }
        }

        private User Rank(User user, string funktion)
        {
            if (funktion == "Befördern")
            {
                if (user.Roles == "User")
                {
                    user.Roles = "Support";
                    return user;
                }
                if (user.Roles == "Support")
                {
                    user.Roles = "Admin";
                    return user;
                }
            }
            if (funktion == "Dekradieren")
            {
                if (user.Roles == "Admin")
                {
                    user.Roles = "Support";
                    return user;
                }
                if (user.Roles == "Support")
                {
                    user.Roles = "User";
                    return user;
                }
            }
            return user;
        }
    }
}