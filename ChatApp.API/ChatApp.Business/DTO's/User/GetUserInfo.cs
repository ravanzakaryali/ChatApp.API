using System;

namespace ChatApp.Business.DTO_s.User
{
    public class GetUserInfo
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Bio { get; set; }
        public bool IsActive { get; set; }
        public string Avatar { get; set; }
        public DateTime LastSeenDate { get; set; }
    }
}
