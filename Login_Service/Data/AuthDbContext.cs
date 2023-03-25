using Login_Service.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Login_Service.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
