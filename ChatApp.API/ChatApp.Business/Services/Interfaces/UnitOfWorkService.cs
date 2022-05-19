using ChatApp.Business.Services.Implementations;
using ChatApp.Core;

namespace ChatApp.Business.Services.Interfaces
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private IMessageService _messageService;
        public IMessageService MessageService => _messageService ??= new MessageService(_unitOfWork);
    }
}
