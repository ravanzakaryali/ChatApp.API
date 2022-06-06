using ChatApp.Business.DTO_s.Message;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatApp.Business.Interfaces
{
    public interface IChatClient 
    {
        Task ReceiveMessage(GetMessage message);
        Task GetClients(List<string> clients);
        Task GetConnectionId(string connectionId);
    }
}
