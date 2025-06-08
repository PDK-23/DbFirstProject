using DbFirstProject.Infrastructure.Entities;
using DbFirstProject.Infrastructure.Repositories;

namespace DbFirstProject.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbFirstProjectDbContext _context;
        public IRepository<User> Users { get; }
        public IRepository<Product> Products { get; }
        public IRepository<Role> Roles { get; }

        public UnitOfWork(DbFirstProjectDbContext context)
        {
            _context = context;
            Users = new Repository<User>(_context);
            Products = new Repository<Product>(_context);
            Roles = new Repository<Role>(_context);
        }

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();
    }
}
