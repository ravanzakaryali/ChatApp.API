using ChatApp.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Data.DataAccess
{

    public class DbContext : IdentityDbContext<User>
    {
        public DbContext(DbContextOptions<DbContext> options) : base(options) { }
        public DbSet<Message> Messages { get; set; }  
        

    }
}
