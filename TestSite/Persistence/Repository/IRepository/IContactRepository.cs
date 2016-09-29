using System.Collections.Generic;
using TestSite.Models;

namespace TestSite.Persistence.Repository.IRepository
{
    internal interface IContactRepository
    {
        IEnumerable<Contact> GetUser();
        void DeleteUserById(int userId);
        void ChangeStatus(int userId, int newStatus);
    }
}