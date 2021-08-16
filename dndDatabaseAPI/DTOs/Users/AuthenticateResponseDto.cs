using dndDatabaseAPI.Models.Users;
using System;
using System.Text.Json.Serialization;

namespace dndDatabaseAPI.DTOs.Users
{
    public record AuthenticateResponseDto
    {
        public Guid Id { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Username { get; init; }
        public string JwtToken { get; init; }

        [JsonIgnore] // refresh token is returned in http only cookie
        public string RefreshToken { get; init; }

        public AuthenticateResponseDto(User user, string jwtToken, string refreshToken)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Username = user.Username;
            JwtToken = jwtToken;
            RefreshToken = refreshToken;
        }
    }
}
