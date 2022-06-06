using System;

namespace ChatApp.Business.DTO_s.Message
{
    public class MessageDto
    {
        public string Content { get; set; }
        public string SendUserId { get; set; }
        public DateTime SenderDate { get; set; } = DateTime.Now;
    }
}
