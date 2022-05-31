using ChatApp.API.Hubs;
using ChatApp.API.Interfaces;
using ChatApp.Business.DTO_s.Common;
using ChatApp.Business.DTO_s.Message;
using ChatApp.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWork;
        private readonly IHubContext<ChatHub, IChatClient> _hubContext;
        public ChatController(IHubContext<ChatHub, IChatClient> hubContext, IUnitOfWorkService unitOfWork)
        {
            _hubContext = hubContext;
            _unitOfWork = unitOfWork;   
        }
        [HttpPost]
        public async Task<ActionResult> SendMessage(MessageDto message)
        {
            try
            {
                await _hubContext.Clients.User(message.SendUserId).ReceiveMessage(message.Content);
                await _unitOfWork.MessageService.SendMessage(message);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
        [HttpGet("{username}")]
        public async Task<ActionResult<List<MessageDto>>> GetMessages(string username)
        {
            try
            {
                return Ok(await _unitOfWork.MessageService.GetMessages(username));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
    }
}
