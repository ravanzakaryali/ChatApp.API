using ChatApp.API.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatApp.API.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {
        private static readonly List<string> _clients = new List<string>();
        public override async Task OnConnectedAsync()
        {
            _clients.Add(Context.ConnectionId);
            await Clients.All.GetClients(_clients);
            await Clients.Caller.GetConnectionId(Context.ConnectionId);
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            _clients.Remove(Context.ConnectionId);
            await Clients.All.GetClients(_clients);
        }
        public async Task SendClientMessage(string message,string connectionId)
        {
            await Clients.Client(connectionId).ReceiveMessage(message);
        }
        public async Task AddGroup(string connectionId, string groupName)
        {
            await Groups.AddToGroupAsync(connectionId, groupName);
        }
        public async Task SendGroupMessage(string message, string groupName)
        {
            await Clients.Group(groupName).ReceiveMessage(message);
        }
        //It's in the controller
        //public async Task SendMessageAsync(string message)
        //{
        //    await Clients.All.SendAsync("receiveMessage", message);
        //}
    }
}
