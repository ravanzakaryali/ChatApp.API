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
        public async Task<List<GetMessage>> GetMessages(string username)
        {
            User user = await _unitOfWork.UserRepository.GetAsync(u => u.UserName == username);
            if (user is null)
                    throw new NotFoundException("User is not defined");
            var loginUserId = _httpContext.HttpContext.User.GetUserId();
            var messagesDb = await _unitOfWork.MessageRepository.GetAllPaginateAsync(1,10,m=>m.SenderDate,m=>m.SendUserId == user.Id && m.UserId == loginUserId);
            return _mapper.Map<List<GetMessage>>(messagesDb);
        }
    }
}
