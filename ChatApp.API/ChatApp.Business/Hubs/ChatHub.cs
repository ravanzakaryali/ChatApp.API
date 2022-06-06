using ChatApp.Business.Extensions;
using ChatApp.Business.Interfaces;
using ChatApp.Data.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Business.Hubs
{
    [Authorize]
    public class ChatHub : Hub<IChatClient>
    {
        private static readonly List<string> _clients = new List<string>();
        private readonly Data.DataAccess.DbContext _context;
        private readonly IHttpContextAccessor _httpContext;
        public ChatHub(
            Data.DataAccess.DbContext context,
            IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        public override async Task OnConnectedAsync()
        {
            var name = Context.User.Identity.Name;
            var user = await _context.Users.Where(u => u.UserName == name).FirstOrDefaultAsync();
            user.LastSeenDate = DateTime.UtcNow;
            user.IsActive = true;
            _context.Update(user);
            _context.SaveChanges();
            //_clients.Add(Context.ConnectionId);
            //await Clients.All.GetClients(_clients);
            //await Clients.Caller.GetConnectionId(Context.ConnectionId);
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            var name = Context.User.Identity.Name;
            var user = _context.Users.Where(u => u.UserName == name).FirstOrDefault();
            user.LastSeenDate = DateTime.UtcNow;
            user.IsActive = false;
            _context.Update(user);
            _context.SaveChanges();
            return base.OnDisconnectedAsync(exception);

        }
        //public async Task SendClientMessage(string message, string connectionId)
        //{
        //    await Clients.Client(connectionId).ReceiveMessage(message);
        //}
        //public async Task AddGroup(string connectionId, string groupName)
        //{
        //    await Groups.AddToGroupAsync(connectionId, groupName);
        //}
        //public async Task SendGroupMessage(string message, string groupName)
        //{
        //    await Clients.Group(groupName).ReceiveMessage(message);
        //}
    }
}
