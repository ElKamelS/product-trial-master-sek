using PROJECT.Application.Interfaces;
using PROJECT.Infrastructure.Repositories;

namespace PROJECT.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProjectDbContext _context;

        public IProductRepository Products { get; }

        public UnitOfWork(ProjectDbContext context)
        {
            _context = context;
            Products = new ProductRepository(_context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
