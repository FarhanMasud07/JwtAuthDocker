using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace JwtAuthenticationProject.Models
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }

        public string UserName { get; set; } = null!;

        public byte[]? PasswordHash { get; set; } = null!;

        public byte[]? PasswordSalt { get; set; } = null!;

        public string Password { get; set; } = null!;

        public bool IsSubscribed { get; set; }

        [Required]
        public string PhoneNumber { get; set; } = null!;

        [Required, NotNull, EmailAddress]
        public string Email { get; set; } = null!;

        public bool IsDarkMode { get; set; }


    }
}
