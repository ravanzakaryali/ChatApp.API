using AutoMapper;
using ChatApp.Business.DTO_s.Autheticate;
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

        public async Task Register(Register register)
        {
            User isEmail = await _userManager.FindByNameAsync(register.Email);
            //Exception not  found exception
            IdentityResult result = await _userManager.CreateAsync(_mapper.Map<User>(register), register.Password);
            //Exception
            //Added Role
        }
    }
}
