using ChatApp.Business.DTO_s.Autheticate;
using ChatApp.Business.DTO_s.Errors;
using System.Threading.Tasks;

namespace ChatApp.Business.Services.Interfaces
{
    public interface IUserService
    {
        Task<RegisterResult> Register(Register register);
    }
}
