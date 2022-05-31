using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Business.DTO_s.User
{
    public class GetUser
    {
        public string Username { get; set; }
        public string Avatar { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsActive { get; set; }
             
    }
}
