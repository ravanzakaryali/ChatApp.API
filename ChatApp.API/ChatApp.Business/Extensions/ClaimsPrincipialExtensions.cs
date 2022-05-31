using System;
using System.Security.Claims;

namespace ChatApp.Business.Extensions
{
    public static class ClaimsPrincipialExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal is null)
                throw new ArgumentNullException(nameof(principal));
            return principal.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
