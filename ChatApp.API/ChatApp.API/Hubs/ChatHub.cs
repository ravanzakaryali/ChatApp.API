using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ChatApp.API.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessageAsync(string message)
        {
            await Clients.All.SendAsync("receiveMessage",message);
        }
    }
}
