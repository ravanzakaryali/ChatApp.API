using AutoMapper;
using ChatApp.Business.Services.Interfaces;
using ChatApp.Core;

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
    }
}
