using System.ComponentModel.DataAnnotations;

namespace TSWD.EducationManagement.Domain.DTOs.Auth
{
    public class LoginRequest
    {
        [Required]
        public string Email { get; set; } = null!;

        [MinLength(8)]
        [Required]
        public string Password { get; set; } = null!;

        public string? TenantName { get; set; }
    }
}
