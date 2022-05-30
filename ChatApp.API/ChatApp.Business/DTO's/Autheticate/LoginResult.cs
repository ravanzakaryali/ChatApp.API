﻿using System;
using System.IdentityModel.Tokens.Jwt;

namespace ChatApp.Business.DTO_s.Autheticate
{
    public class LoginResult
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
