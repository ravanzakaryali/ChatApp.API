using Microsoft.AspNetCore.Identity;

namespace ChatApp.Business.DTO_s.Errors
{
    public class RegisterResult
    {
        public IEnumarable<IdentityError> Error { get; set; }
    }
}
