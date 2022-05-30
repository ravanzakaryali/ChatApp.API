using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace ChatApp.Core.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public string Bio { get; set; } 
        public string Avatar { get; set; }
        public ICollection<Message> Messages { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
