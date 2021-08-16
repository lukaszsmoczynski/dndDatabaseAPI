namespace dndDatabaseAPI.DTOs.Spells
{
    public record MaterialComponentDto
    {
        public string Name { get; init; }
        public float? Value { get; init; }
        public bool Consumed { get; init; }
        public string? Description { get; init; }
    }
}