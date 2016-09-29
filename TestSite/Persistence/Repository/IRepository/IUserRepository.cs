using TestSite.Models;

namespace TestSite.Persistence.Repository.IRepository
{
    public interface IUserRepository
    {
        User FindMail(string mailadress);
    }
}