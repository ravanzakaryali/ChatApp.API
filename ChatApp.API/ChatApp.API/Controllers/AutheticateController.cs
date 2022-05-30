using ChatApp.Business.DTO_s.Autheticate;
using ChatApp.Business.DTO_s.Common;
using ChatApp.Business.Services.Interfaces;
using ChatApp.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ChatApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutheticateController : ControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWork;

        public AutheticateController(IUnitOfWorkService unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login(Login login)
        {
            try
            {
                return Ok(await _unitOfWork.UserService.Login(login));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message});
            }
        }
        [HttpPost("register")]
        public async Task<ActionResult> Register(Register register)
        {
            try
            {
                return Ok(await _unitOfWork.UserService.Register(register));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
        [HttpPost("refresh-token")]
        public async Task<ActionResult> RefreshToken(TokenModel tokenModel)
        {
            try
            {
                return Ok(await _unitOfWork.UserService.RefreshToken(tokenModel));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });      
            }
        }
        [HttpPost("create-roles")]
        public async Task<ActionResult> CreateRole()
        {
            try
            {
                await _unitOfWork.UserService.CreateRoles();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
    }
}
