using ChatApp.Core.Entities;
using ChatApp.Core.Interface;
using ChatApp.Data.DataAccess;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.Data.Implementations
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly DbContext _context;
        public UserRepository(DbContext context) : base(context)
        {
            _context = context;
        }
    }
}
