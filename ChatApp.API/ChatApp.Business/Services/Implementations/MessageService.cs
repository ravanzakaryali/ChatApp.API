using ChatApp.Business.Services.Interfaces;
using ChatApp.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Business.Services.Implementations
{
    internal class MessageService : IMessageService
    {
        public readonly IUnitOfWork _unitOfWork;

        public MessageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
