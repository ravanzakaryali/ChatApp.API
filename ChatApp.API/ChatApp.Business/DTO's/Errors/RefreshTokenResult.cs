using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Business.DTO_s.Errors
{
    public class RefreshTokenResult
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
