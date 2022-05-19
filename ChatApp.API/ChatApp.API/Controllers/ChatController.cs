using ChatApp.API.Hubs;
using ChatApp.API.Interfaces;
using ChatApp.Business.DTO_s.Message;
using ChatApp.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IHubContext<ChatHub, IChatClient> _hubContext;
        public ChatController(IHubContext<ChatHub, IChatClient> hubContext)
        {
            _hubContext = hubContext;
        }
        [HttpPost]
        public async Task<ActionResult> SendMessage(MessageDto message)
        {
            await _hubContext.Clients.All.ReceiveMessage(message.Message);
            return Ok();
        }
    }
}
