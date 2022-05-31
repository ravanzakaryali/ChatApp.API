using AutoMapper;
using ChatApp.Business.DTO_s.Message;
using ChatApp.Business.Services.Interfaces;
using ChatApp.Core;
using ChatApp.Core.Entities;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using ChatApp.Business.Extensions;
using ChatApp.Business.Exceptions;

namespace ChatApp.Business.Services.Implementations
{
    internal class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public MessageService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContext = httpContext;
        }
        public async Task SendMessage(MessageDto message)
        {
            Message newMessage = _mapper.Map<Message>(message);
            if (await _unitOfWork.UserRepository.GetAsync(u => u.Id == message.SendUserId) is null)
                throw new NotFoundException("User is not defined");
            newMessage.UserId = _httpContext.HttpContext.User.GetUserId();
            await _unitOfWork.MessageRepository.CreateAsync(newMessage);
            await _unitOfWork.SaveAsync();
        }
    }
}
