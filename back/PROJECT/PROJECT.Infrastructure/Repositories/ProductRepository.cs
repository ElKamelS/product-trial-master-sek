using Microsoft.EntityFrameworkCore;
using PROJECT.Application.Interfaces;
using PROJECT.Domain.Entities;
using PROJECT.Infrastructure.Data;


namespace PROJECT.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ProjectDbContext _context;

        public ProductRepository(ProjectDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
