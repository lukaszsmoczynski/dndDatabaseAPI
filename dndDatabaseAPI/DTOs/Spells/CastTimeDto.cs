using System.ComponentModel.DataAnnotations;

namespace dndDatabaseAPI.DTOs.Spells
{
    public enum SpellTimeUnitDto
    {
        BonusAction,
        Reaction,
        Action,
        Round,
        Minute,
        Hour
    }

    public class CastTimeDto
    {
        public int Amount { get; set; }
        [Required]
        public SpellTimeUnitDto Unit { get; set; }
    }
}
