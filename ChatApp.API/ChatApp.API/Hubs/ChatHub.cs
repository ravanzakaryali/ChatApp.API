using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatApp.API.Hubs
{
    public class ChatHub : Hub
    {
        private static readonly List<string> _clients = new List<string>();
        public override async Task OnConnectedAsync()
        {
            _clients.Add(Context.ConnectionId);
            await Clients.All.SendAsync("getClients", _clients);
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            _clients.Remove(Context.ConnectionId);
            await Clients.All.SendAsync("getClients", _clients);
        }
        public async Task SendMessageAsync(string message)
        {
            await Clients.All.SendAsync("receiveMessage", message);
        }
    }
}
