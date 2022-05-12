using ChatApp.Core.Entities;
using ChatApp.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Data.Implementations
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        private readonly DataAccess.DbContext _context; 
        public MessageRepository(DataAccess.DbContext context) : base(context)
        {
            _context = context;
        }
    }
}
