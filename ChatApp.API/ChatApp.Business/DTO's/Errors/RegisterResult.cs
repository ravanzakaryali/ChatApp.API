using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ChatApp.Business.DTO_s.Errors
{
    public class RegisterResult
    {
        public IEnumerable<IdentityError> Error { get; set; }
    }
}
