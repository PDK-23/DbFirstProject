using DbFirstProject.Infrastructure.Entities;
using DbFirstProject.Infrastructure.Repositories;

namespace DbFirstProject.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }
        IRepository<Product> Products { get; }
        IRepository<Role> Roles { get; }
        Task<int> SaveChangesAsync();
    }
}
