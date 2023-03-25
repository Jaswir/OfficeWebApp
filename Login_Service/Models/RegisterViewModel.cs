using System.ComponentModel.DataAnnotations;

namespace Login_Service.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage ="Password and confirmation password did not match")]
        public string ConfirmPassword { get; set; }

    }
}
