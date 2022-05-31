using ChatApp.API.Interfaces;
using ChatApp.Business.Extensions;
using ChatApp.Data.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.API.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {
        private static readonly List<string> _clients = new List<string>();
        private readonly Data.DataAccess.DbContext _context;
        private readonly IHttpContextAccessor _httpContext;
        public ChatHub(Data.DataAccess.DbContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }
        public override async Task OnConnectedAsync()
        {
            var userLoginId = _httpContext.HttpContext.User.GetUserId(); 
            var user = await _context.Users.Where(u=>u.Id == userLoginId).FirstOrDefaultAsync();
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
