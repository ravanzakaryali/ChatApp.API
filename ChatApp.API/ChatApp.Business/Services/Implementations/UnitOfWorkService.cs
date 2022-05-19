using AutoMapper;
using ChatApp.Business.Services.Interfaces;
using ChatApp.Core;
using ChatApp.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace ChatApp.Business.Services.Implementations
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtService _jwtService;
        private readonly IConfiguration _configuration; 
        public UnitOfWorkService(IUnitOfWork unitOfWork, 
                                 UserManager<User> userManager, 
                                 RoleManager<IdentityRole> roleManager, 
                                 IMapper mapper,
                                 IJwtService jwtService,
                                 IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _jwtService = jwtService;
            _configuration = configuration;
        }

        private IMessageService _messageService;
        private IUserService _userService;
        public IMessageService MessageService => _messageService ??= new MessageService(_unitOfWork, _mapper);
        public IUserService UserService => _userService ??= new UserService(_unitOfWork, _userManager, _roleManager, _mapper, _jwtService, _configuration);
    }
}
