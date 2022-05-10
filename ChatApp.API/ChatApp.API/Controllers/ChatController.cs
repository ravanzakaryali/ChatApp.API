using ChatApp.API.Hubs;
using ChatApp.API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
        public async Task<ActionResult> SendMessage(string message)
        {
            await _hubContext.Clients.All.ReceiveMessage(message);
            return Ok();
        }
    }
}
