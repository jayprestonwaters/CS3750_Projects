using System.ComponentModel.DataAnnotations;

namespace Hangman.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? PasswordHash { get; set; }

        [Required]
        public string? Salt { get; set; }
    }
}
