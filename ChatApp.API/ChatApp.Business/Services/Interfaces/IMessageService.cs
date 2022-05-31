using ChatApp.Business.DTO_s.Message;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatApp.Business.Services.Interfaces
{
    public interface IMessageService
    {
        Task SendMessage(MessageDto message);
        Task<List<GetMessage>> GetMessages(string username);
    }
}
