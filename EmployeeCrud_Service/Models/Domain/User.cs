using System.ComponentModel.DataAnnotations;

namespace EmployeeCrud_Service.Models.Domain
{
    public class User
    {

        [Key]
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
