using PROJECT.Application.Interfaces;
using PROJECT.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJECT.Application.Services
{
    public class ProductService(IUnitOfWork unitOfWork) : IProductService
    {
        public async Task<IEnumerable<Product>> GetAllAsync()
            => await unitOfWork.Products.GetAllAsync();

        public async Task<Product?> GetByIdAsync(int id)
            => await unitOfWork.Products.GetByIdAsync(id);

        public async Task<Product> CreateAsync(Product product)
        {
            await unitOfWork.Products.AddAsync(product);
            await unitOfWork.SaveChangesAsync();
            return product;
        }

        public async Task<bool> UpdateAsync(int id, Product product)
        {
            if (id != product.id) return false;
            unitOfWork.Products.Update(product);
            await unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await unitOfWork.Products.GetByIdAsync(id);
            if (existing is null) return false;

            unitOfWork.Products.Delete(existing);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
