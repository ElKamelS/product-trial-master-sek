using PROJECT.Application.Interfaces;
using PROJECT.Domain.Entities;
using PROJECT.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJECT.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ProjectDbContext _context;

        public UserRepository(ProjectDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
