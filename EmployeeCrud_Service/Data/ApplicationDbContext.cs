using Microsoft.EntityFrameworkCore;
using EmployeeCrud_Service.Models.Domain;

namespace EmployeeCrud_Service.Data
{
    public class ApplicationDbContext: DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

    }
}
