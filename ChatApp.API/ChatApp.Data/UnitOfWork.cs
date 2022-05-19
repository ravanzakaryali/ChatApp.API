using ChatApp.Core;
using ChatApp.Core.Interface;
using ChatApp.Data.Implementations;
using System.Threading.Tasks;

namespace ChatApp.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataAccess.DbContext _context;
        public UnitOfWork(DataAccess.DbContext context)
        {
            _context = context;
        }

        private IMessageRepository _messageRepository;
        private IUserRepository _userRepository;
        public IMessageRepository MessageRepository => _messageRepository ??= new MessageRepository(_context);
        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
