using ChatApp.Business.Services.Interfaces;
using ChatApp.Core;
using ChatApp.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.Business.Services.Implementations
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public UnitOfWorkService(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        private IMessageService _messageService;
        private IUserService _userService;
        public IMessageService MessageService => _messageService ??= new MessageService(_unitOfWork);
        public IUserService UserService => _userService ??= new UserService(_unitOfWork, _userManager);
    }
}
