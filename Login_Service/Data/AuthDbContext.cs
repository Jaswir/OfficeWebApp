using Login_Service.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Login_Service.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
