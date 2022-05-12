using System;

namespace ChatApp.Core.Entites
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime SenderDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool UserId { get; set; }
        public User User { get; set; }
    }
}
