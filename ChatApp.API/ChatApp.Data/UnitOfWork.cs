using ChatApp.Core;
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
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
