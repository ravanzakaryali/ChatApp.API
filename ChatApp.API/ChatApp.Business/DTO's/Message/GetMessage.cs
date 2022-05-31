using System;

namespace ChatApp.Business.DTO_s.Message
{
    public class GetMessage
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime SenderDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
