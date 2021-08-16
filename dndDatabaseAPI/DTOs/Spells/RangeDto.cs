using System.ComponentModel.DataAnnotations;

namespace dndDatabaseAPI.DTOs.Spells
{
    public record RangeDto
    {
        public int Amount { get; init; }
        [Required]
        public SpellRangeUnitDto Unit { get; init; }
    }
}
