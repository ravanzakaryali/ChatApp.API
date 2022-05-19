using ChatApp.Core.Entities;
using ChatApp.Core.Interface;
using ChatApp.Data.DataAccess;

namespace ChatApp.Data.Implementations
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        private readonly DbContext _context; 
        public MessageRepository(DbContext context) : base(context)
        {
            _context = context;
        }
    }
}
