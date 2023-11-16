using System.ComponentModel.DataAnnotations;

namespace IndentityLogic.DTOs
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{4,22}$", ErrorMessage = "Password must contain a lowercase letter, a capital letter, " +
            "a number and a special symbol. Character range: 4-22")]
        public string Password { get; set; }

        [Required]
        [MaxLength(60)]
        public string Username { get; set; }

        [Required]
        [MaxLength(50)]
        public string DisplayName { get; set; }
    }
}
