using AutoMapper;
using ChatApp.Business.DTO_s.Message;
using ChatApp.Business.Services.Interfaces;
using ChatApp.Core;
using ChatApp.Core.Entities;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ChatApp.Business.Services.Implementations
{
    internal class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MessageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task SendMessage(MessageDto message)
        {
            Message newMessage = _mapper.Map<Message>(message);
            //login user id 
            //newMessage.UserId = 
        }
    }
}
