using ChatApp.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ChatApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutheticateController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public AutheticateController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<ActionResult> CreateUser()
        {
            var user = new User
            {
                Name = "Revan",
                Surname = "Zakaryali",
                Address = "Azerbaijan",
                Email = "r@gmail.com",
                UserName = "revanzli"
            };
            var result = await _userManager.CreateAsync(user, "Rz123456@");
            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    return StatusCode(StatusCodes.Status403Forbidden, new { status = error.Code, message = error.Description });
                }
            }
            return NoContent();
        }
    }
}
