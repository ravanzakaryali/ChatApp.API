using ChatApp.Business.DTO_s.Message;
using System.Threading.Tasks;

namespace ChatApp.Business.Services.Interfaces
{
    public interface IMessageService
    {
        Task SendMessage(MessageDto message);
    }
}
