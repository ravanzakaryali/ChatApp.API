using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ChatApp.Business.Services.Interfaces
{
    public interface IJwtService
    {
        public JwtSecurityToken CreateToken(List<Claim> authClaims);
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
