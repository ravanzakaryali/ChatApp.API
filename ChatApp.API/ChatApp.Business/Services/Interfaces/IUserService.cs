using ChatApp.Business.DTO_s.Autheticate;
using ChatApp.Business.DTO_s.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatApp.Business.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<GetUserDto>> GetUsers(int page,int size);
        Task<RegisterResult> Register(Register register);
        Task<LoginResult> Login(Login login);
        Task<RefreshTokenResult> RefreshToken(TokenModel tokenModel);
        Task CreateRoles();
    }
}
