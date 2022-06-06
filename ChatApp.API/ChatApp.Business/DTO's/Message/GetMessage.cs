using ChatApp.Business.DTO_s.User;
using System;

namespace ChatApp.Business.DTO_s.Message
{
    public class GetMessage
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime SenderDate { get; set; }
        public GetUserInfo SendUser  { get; set; }
    }
}
