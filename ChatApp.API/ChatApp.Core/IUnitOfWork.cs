using ChatApp.Core.Interface;
using System.Threading.Tasks;

namespace ChatApp.Core
{
    public interface IUnitOfWork
    {
        IMessageRepository MessageRepository { get; }
        IUserRepository UserRepository { get; }
        Task SaveAsync();
    }
}
