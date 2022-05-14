using ChatApp.API.Hubs;
using ChatApp.API.Interfaces;
using ChatApp.Business.DTO_s.Message;
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
            ConnectionFactory factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
            using IConnection connection = factory.CreateConnection();
            using IModel chanel = connection.CreateModel();

            chanel.QueueDeclare("messagequeue", true, false, false);
            byte[] data = Encoding.UTF8.GetBytes(message.Message);

            chanel.BasicPublish("", "messagequeue",body: data);

            await _hubContext.Clients.All.ReceiveMessage(message.Message);
            return Ok();
        }
    }
}
