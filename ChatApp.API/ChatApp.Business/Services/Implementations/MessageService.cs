using AutoMapper;
using ChatApp.Business.DTO_s.Message;
using ChatApp.Business.Services.Interfaces;
using ChatApp.Core;
using ChatApp.Core.Entities;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using ChatApp.Business.Extensions;
using System.Collections.Generic;
using ChatApp.Business.Exceptions;
using Microsoft.AspNetCore.SignalR;
using ChatApp.Business.Interfaces;
using ChatApp.Business.Hubs;
using ChatApp.Business.DTO_s.User;

namespace ChatApp.Business.Services.Implementations
{
    internal class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IHubContext<ChatHub, IChatClient> _hubContext;


        public MessageService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContext, IHubContext<ChatHub, IChatClient> hubContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContext = httpContext;
            _hubContext = hubContext;

        }
        public async Task<GetMessage> SendMessage(MessageDto message)
        {
            var sendUser = await _unitOfWork.UserRepository.GetAsync(u => u.Id == message.SendUserId);
            if (await _unitOfWork.UserRepository.GetAsync(u => u.Id == message.SendUserId) is null)
                throw new NotFoundException("User is not defined");
            GetMessage getMessage = new GetMessage()
            {
                Content = message.Content,
                SenderDate = System.DateTime.Now,
                SendUser = _mapper.Map<GetUserInfo>(sendUser),
            };
            await _hubContext.Clients.User(sendUser.Id).ReceiveMessage(getMessage);
            Message newMessage = _mapper.Map<Message>(message);
            newMessage.UserId = _httpContext.HttpContext.User.GetUserId();
            await _unitOfWork.MessageRepository.CreateAsync(newMessage);
            await _unitOfWork.SaveAsync();
            return getMessage;
        }
        public async Task<List<GetMessage>> GetMessages(string username)
        {
            User user = await _unitOfWork.UserRepository.GetAsync(u => u.UserName == username);
            if (user is null)
            {
                throw new NotFoundException("User is not defined");
            }
            var loginUserId = _httpContext.HttpContext.User.GetUserId();
            List<Message> messagesDb = await _unitOfWork.MessageRepository.GetAllPaginateAsync(1,50,m=>m.SenderDate,m=>(m.SendUserId == loginUserId && m.UserId == user.Id) || (m.SendUserId == user.Id && m.UserId == loginUserId),"SendUser");
            return _mapper.Map<List<GetMessage>>(messagesDb);
        }
    }
}
