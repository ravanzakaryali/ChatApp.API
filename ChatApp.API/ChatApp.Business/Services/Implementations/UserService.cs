using AutoMapper;
using ChatApp.Business.DTO_s.Autheticate;
using ChatApp.Business.DTO_s.Errors;
using ChatApp.Business.Exceptions;
using ChatApp.Business.Services.Interfaces;
using ChatApp.Core;
using ChatApp.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace ChatApp.Business.Services.Implementations
{
    public class UserService : IUserService
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, UserManager<User> userManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<RegisterResult> Register(Register register)
        {
            RegisterResult registerResult = new RegisterResult();
            User isEmail = await _userManager.FindByNameAsync(register.Email);
            if (isEmail != null) throw new AlreadyExistsException("Already exception");
            IdentityResult result = await _userManager.CreateAsync(_mapper.Map<User>(register), register.Password);
            if (!result.Succeeded)
            {
                registerResult.Error = result.Errors;
                return registerResult;
            };
            return registerResult;
        }
    }
}
