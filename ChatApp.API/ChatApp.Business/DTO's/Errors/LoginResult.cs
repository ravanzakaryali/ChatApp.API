using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ChatApp.Business.DTO_s.Errors
{
    public class LoginResult
    {
        public JwtSecurityToken Token { get; set; }
        public string RefreshToken { get; set; }
        public string Expiration { get; set; }
    }
}
