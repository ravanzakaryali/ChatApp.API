﻿using ChatApp.Business.Extensions;
using ChatApp.Business.Interfaces;
using ChatApp.Core.Entities;
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
        private static readonly List<User> _clients = new List<User>();
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
            if (!_clients.Any(u => u.Name == name))
            {   
                _clients.Add(user);
            }
            await Clients.All.GetClients(_clients);
            await Clients.All.GetClient(user);
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var name = Context.User.Identity.Name;
            var user = _context.Users.Where(u => u.UserName == name).FirstOrDefault();
            user.LastSeenDate = DateTime.UtcNow;
            user.IsActive = false;
            _context.Update(user);
            _context.SaveChanges();
            _clients.RemoveAll(u => u.UserName == name);
            await Clients.All.GetClients(_clients);
            await Clients.All.GetClient(user);
        }
    }
}
