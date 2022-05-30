using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Business.DTO_s.User
{
    public class GetUserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Bio { get; set; }
        public bool IsActive { get; set; }
    }
}
