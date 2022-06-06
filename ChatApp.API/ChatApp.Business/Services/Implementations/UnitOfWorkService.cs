using AutoMapper;
using ChatApp.Business.Hubs;
using ChatApp.Business.Interfaces;
using ChatApp.Business.Services.Interfaces;
using ChatApp.Core;
using ChatApp.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
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
        private readonly IHubContext<ChatHub, IChatClient> _hubContext;

        public UnitOfWorkService(IUnitOfWork unitOfWork, 
                                 UserManager<User> userManager, 
                                 RoleManager<IdentityRole> roleManager, 
                                 IMapper mapper,
                                 IJwtService jwtService,
                                 IConfiguration configuration,
                                 IHttpContextAccessor httpContext,
                                 IHubContext<ChatHub, IChatClient> hubContext)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _jwtService = jwtService;
            _configuration = configuration;
            _httpContext = httpContext;
            _hubContext = hubContext;
        }

        private IMessageService _messageService;
        private IUserService _userService;
        public IMessageService MessageService => _messageService ??= new MessageService(_unitOfWork, _mapper, _httpContext,_hubContext);
        public IUserService UserService => _userService ??= new UserService(_unitOfWork, _userManager, _roleManager, _mapper, _jwtService, _configuration);
    }
}
