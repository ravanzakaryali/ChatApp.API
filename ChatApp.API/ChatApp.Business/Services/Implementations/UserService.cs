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
        public UserService(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task Register(Register register)
        {
            User isEmail = await _userManager.FindByNameAsync(register.Email);
            //Exception not  found exception
            User user = new User
            {
                Name = register.Name,
                Email = register.Email,
                Address = register.Address,
                Bio = register.Bio,
            };
            IdentityResult result = await _userManager.CreateAsync(user, register.Password);
            //Exception
            //Added Role
        }
    }
}
