using dndDatabaseAPI.DTOs.Character;
using dndDatabaseAPI.DTOs.Dices;
using dndDatabaseAPI.DTOs.Spells;
using dndDatabaseAPI.Models.Characters.Classes;
using dndDatabaseAPI.Models.Dices;
using dndDatabaseAPI.Models.Spells;
using System.Linq;

namespace dndDatabaseAPI.Helpers
{
    public static class Extensions
    {
        public static SpellDto AsDto(this Spell spell)
        {
            return new SpellDto
            {
                Id = spell.Id,
                Name = spell.Name,
                Level = spell.Level,
                School = spell.School,
                CastTime = spell.CastTime.AsDto(),
                Range = spell.Range.AsDto(),
                Components = spell.Components,
                Duration = spell.Duration,
                Description = spell.Description,
                Rolls = spell.Rolls.Select(roll => roll.AsDto()).ToList(),
                Classes = spell.Classes,
                Ritual = spell.Ritual,
                Upcasting = spell.Upcasting
            };
        }
        public static CastTimeDto AsDto(this CastTime castTime)
        {
            return new CastTimeDto
            {
                Amount = castTime.Amount,
                Unit = (SpellTimeUnitDto)castTime.Unit
            };
        }
        public static RangeDto AsDto(this Models.Spells.Range range)
        {
            return new RangeDto
            {
                Amount = range.Amount,
                Unit = (SpellRangeUnitDto)range.Unit
            };
        }
        public static RollDto AsDto(this Roll roll)
        {
            return new RollDto
            {
                Dices = roll.Dices,
                Description = roll.Description
            };
        }
        public static ClassDto AsDto(this ICharacterClass characterClass)
        {
            return new ClassDto
            {
                Id = characterClass.Id,
                Name = characterClass.Name,
                Description= characterClass.Description
            };
        }
    }
}
