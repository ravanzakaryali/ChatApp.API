using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatApp.API.Interfaces
{
    public interface IChatClient 
    {
        Task ReceiveMessage(string message);
        Task GetClients(List<string> clients);
        Task GetConnectionId(string connectionId);
    }
}
