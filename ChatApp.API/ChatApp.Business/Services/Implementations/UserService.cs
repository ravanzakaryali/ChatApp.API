using AutoMapper;
using ChatApp.Business.DTO_s.Autheticate;
using ChatApp.Business.DTO_s.Errors;
using ChatApp.Business.Exceptions;
using ChatApp.Business.Helpers;
using ChatApp.Business.Services.Interfaces;
using ChatApp.Core;
using ChatApp.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace ChatApp.Business.Services.Implementations
{
    public class UserService : IUserService
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;
        private readonly IConfiguration _configuration;

        public UserService(IUnitOfWork unitOfWork,
                           UserManager<User> userManager,
                           RoleManager<IdentityRole> roleManager,
                           IMapper mapper,
                           IJwtService jwtService,
                           IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
            _jwtService = jwtService;
            _configuration = configuration;
        }

        public async Task<RegisterResult> Register(Register register)
        {
            RegisterResult registerResult = new RegisterResult();
            User isEmail = await _userManager.FindByNameAsync(register.Email);
            if (isEmail != null) throw new AlreadyExistsException("Already exception");
            User user = _mapper.Map<User>(register);
            IdentityResult result = await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
            {
                registerResult.Error = result.Errors;
                return registerResult;
            };
            await _userManager.AddToRoleAsync(user, Roles.Member.ToString());
            return registerResult;
        }

        public async Task<LoginResult> Login(Login login)
        {
            User user = await _userManager.FindByNameAsync(login.UserName);
            if (user is null) throw new NotFoundException("User not found");
            if (!await _userManager.CheckPasswordAsync(user, login.Password)) throw new UnauthorizedAccessException();
            var roles = _userManager.GetRolesAsync(user).Result;
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                };
            authClaims.AddRange(roles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));
            JwtSecurityToken token = _jwtService.CreateToken(authClaims);
            var refreshToken = GenerateRefreshToken();
            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);
            user.RefreshToken = refreshToken;
            await _userManager.UpdateAsync(user);
            return new LoginResult
            {
                Token = token,
                RefreshToken = refreshToken,
                Expiration = token.ValidTo
            };
        }
        #region CreateRoles
        public async Task CreateRoles()
        {
            foreach (var item in Enum.GetValues(typeof(Roles)))
            {
                if (!(await _roleManager.RoleExistsAsync(item.ToString())))
                {
                    await _roleManager.CreateAsync(new IdentityRole
                    {
                        Name = item.ToString()
                    });
                }
            }
        }
        #endregion

        public async Task<RefreshTokenResult> RefreshToken(TokenModel tokenModel)
        {
            if (tokenModel is null) throw new NullReferenceException(nameof(tokenModel));
            string accessToken = tokenModel.AccessToken;
            string refreshToken = tokenModel.RefreshToken;
            var principal = _jwtService.GetPrincipalFromExpiredToken(accessToken);
            if (principal == null) throw new UnauthorizedAccessException();
            string username = principal.Identity.Name;
            var user = await _userManager.FindByNameAsync(username);
            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                throw new NullReferenceException();
            var newAccessToken = _jwtService.CreateToken(principal.Claims.ToList());
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return new RefreshTokenResult
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken
            };
        }
        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
