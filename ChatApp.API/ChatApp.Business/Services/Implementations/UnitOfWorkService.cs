using AutoMapper;
using ChatApp.Business.Services.Interfaces;
using ChatApp.Core;
using ChatApp.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.Business.Services.Implementations
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UnitOfWorkService(IUnitOfWork unitOfWork, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        private IMessageService _messageService;
        private IUserService _userService;
        public IMessageService MessageService => _messageService ??= new MessageService(_unitOfWork, _mapper);
        public IUserService UserService => _userService ??= new UserService(_unitOfWork, _userManager, _roleManager, _mapper);
    }
}
