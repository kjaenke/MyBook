using System;

namespace TestSite.Persistence.Repository.IRepository

{
    public interface IUnitOfWork : IDisposable
    {
        UserRepository Users { get; }

        int Complete();
    }
}