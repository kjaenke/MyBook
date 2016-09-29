using System.Collections.Generic;
using System.Data.Entity;
using TestSite.Infrastructure;
using TestSite.Models;
using TestSite.Persistence.Repository.IRepository;

namespace TestSite.Persistence.Repository
{
    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        public ContactRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<Contact> GetUser()
        {
            UnitOfWork unit = new UnitOfWork(new PlutoContext());
            IEnumerable<Contact> contactList = unit.Contacts.GetAll();

            return contactList;
        }

        public void DeleteUserById(int userId)
        {
            UnitOfWork unit = new UnitOfWork(new PlutoContext());
            Contact contact = unit.Contacts.Get(userId);
            unit.Contacts.Remove(contact);
        }

        public void ChangeStatus(int userId, int newStatus)
        {
            UnitOfWork unit = new UnitOfWork(new PlutoContext());
            Contact contact = unit.Contacts.Get(userId);
            contact.Status = newStatus;

            unit.Contacts.Add(contact);

            unit.Complete();
        }
    }
}