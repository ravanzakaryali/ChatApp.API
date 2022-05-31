using ChatApp.Business.DTO_s.Autheticate;
using ChatApp.Business.DTO_s.Common;
using ChatApp.Business.DTO_s.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatApp.Business.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<GetUser>> GetUsers(PaginateQuery query);
        Task<GetUserInfo> GetUser(string username);
        Task<RegisterResult> Register(Register register);
        Task<LoginResult> Login(Login login);
        Task<RefreshTokenResult> RefreshToken(TokenModel tokenModel);
        Task CreateRoles();
    }
}
