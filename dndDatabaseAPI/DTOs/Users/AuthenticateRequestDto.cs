using System.ComponentModel.DataAnnotations;

namespace dndDatabaseAPI.DTOs.Users
{
    public record AuthenticateRequestDto
    {
        [Required]
        public string Username { get; init; }
        [Required]
        public string Password { get; init; }
    }
}
