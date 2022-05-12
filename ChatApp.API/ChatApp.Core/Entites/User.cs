using Microsoft.AspNetCore.Identity;

namespace ChatApp.Core.Entites
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public string Bio { get; set; }
    }
}
