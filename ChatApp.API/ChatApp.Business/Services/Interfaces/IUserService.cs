using ChatApp.Business.DTO_s.Autheticate;
using System.Threading.Tasks;

namespace ChatApp.Business.Services.Interfaces
{
    public interface IUserService
    {
        Task<RegisterResult> Register(Register register);
        Task<LoginResult> Login(Login login);
        Task<RefreshTokenResult> RefreshToken(TokenModel tokenModel);
        Task CreateRoles();
    }
}
