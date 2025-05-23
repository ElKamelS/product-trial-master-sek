using PROJECT.Application.Interfaces;
using PROJECT.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJECT.Application.Services
{
    public class UserService(IUnitOfWork unitOfWork) : IUserService
    {
        public async Task<IEnumerable<User>> GetAllAsync()
            => await unitOfWork.Users.GetAllAsync();

        public async Task<User?> GetByIdAsync(int id)
            => await unitOfWork.Users.GetByIdAsync(id);

        public async Task<User> CreateAsync(User user)
        {
            await unitOfWork.Users.AddAsync(user);
            await unitOfWork.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UpdateAsync(int id, User user)
        {
            if (id != user.id) return false;
            unitOfWork.Users.Update(user);
            await unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await unitOfWork.Users.GetByIdAsync(id);
            if (existing is null) return false;

            unitOfWork.Users.Delete(existing);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
