namespace dndDatabaseAPI.DTOs.Spells
{
    public record DurationDto
    {
        public bool UpTo { get; set; }
        public int Amount { get; init; }
        public SpellDurationUnitDto Unit { get; init; }
        public bool Contentration { get; init; }
    }
}
