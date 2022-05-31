using ChatApp.Business.DTO_s.Common;
using ChatApp.Business.DTO_s.User;
using ChatApp.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ChatApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWork;

        public UserController(IUnitOfWorkService unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<ActionResult<List<UserInfo>>> GetAll([FromQuery] PaginateQuery query)
        {
            try
            {
                return Ok(await _unitOfWork.UserService.GetUsers(query));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
        [HttpGet("{username}")]
        public async Task<ActionResult<UserInfo>> Get(string username)
        {
            try
            {
                return Ok(await _unitOfWork.UserService.GetUser(username));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        } 
    }
}
