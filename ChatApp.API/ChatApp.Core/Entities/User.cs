using Microsoft.AspNetCore.Identity;
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
        public ICollection<Message> Messages { get; set; }
    }
}
