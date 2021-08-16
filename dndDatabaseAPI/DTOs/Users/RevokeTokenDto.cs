namespace dndDatabaseAPI.DTOs.Users
{
    public record RevokeTokenDto
    {
        public string Token { get; init; }
    }
}
