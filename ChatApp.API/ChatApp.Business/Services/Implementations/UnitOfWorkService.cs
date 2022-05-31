using AutoMapper;
using ChatApp.Business.Services.Interfaces;
using ChatApp.Core;
using ChatApp.Core.Entities;
using Microsoft.AspNetCore.Http;
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
        private IHttpContextAccessor _httpContext;
        public UnitOfWorkService(IUnitOfWork unitOfWork, 
                                 UserManager<User> userManager, 
                                 RoleManager<IdentityRole> roleManager, 
                                 IMapper mapper,
                                 IJwtService jwtService,
                                 IConfiguration configuration,
                                 IHttpContextAccessor httpContext)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _jwtService = jwtService;
            _configuration = configuration;
            _httpContext = httpContext;
        }

        private IMessageService _messageService;
        private IUserService _userService;
        public IMessageService MessageService => _messageService ??= new MessageService(_unitOfWork, _mapper, _httpContext);
        public IUserService UserService => _userService ??= new UserService(_unitOfWork, _userManager, _roleManager, _mapper, _jwtService, _configuration);
    }
}
