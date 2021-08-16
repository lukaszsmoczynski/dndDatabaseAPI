using dndDatabaseAPI.DTOs.Characters.Classes;
using dndDatabaseAPI.DTOs.Dices;
using dndDatabaseAPI.DTOs.Spells;
using dndDatabaseAPI.Models.Characters.Classes;
using dndDatabaseAPI.Models.Dices;
using dndDatabaseAPI.Models.Spells;
using System.Linq;

namespace dndDatabaseAPI.Helpers
{
    public static class DTOConverters
    {
        public static SpellDto AsDto(this Spell spell)
        {
            return new SpellDto
            {
                Id = spell.Id,
                Name = spell.Name,
                Level = spell.Level,
                School = (SchoolDto)spell.School,
                CastTime = spell.CastTime?.AsDto(),
                Range = spell.Range?.AsDto(),
                Components = spell.Components?.AsDto(),
                Duration = spell.Duration.AsDto(),
                Description = spell.Description,
                Rolls = spell.Rolls?.Select(roll => roll.AsDto()).ToList(),
                Classes = spell.Classes,
                Ritual = spell.Ritual,
                Upcasting = spell.Upcasting?.Select(upcasting => upcasting.AsDto()).ToList()
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
        public static RangeDto AsDto(this Range range)
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
        public static UpcastingDto AsDto(this Upcasting upcasting)
        {
            return new UpcastingDto
            {
                Level = upcasting.Level,
                Rolls = upcasting.Rolls?.Select(roll => roll.AsDto())
            };
        }
        public static ClassDto AsDto(this Class characterClass)
        {
            return new ClassDto
            {
                Id = characterClass.Id,
                Name = characterClass.Name,
                Description = characterClass.Description,
                //SubClass = characterClass.SubClass?.AsDto()
            };
        }
        public static CasterClassDto AsDto(this CasterClass casterClass)
        {
            return new CasterClassDto
            {
                Id = casterClass.Id,
                Name = casterClass.Name,
                Description = casterClass.Description,
                //SubClass = casterClass.SubClass?.AsDto(),
            };
        }
        public static SubClassDto AsDto(this SubClass subClass)
        {
            return new SubClassDto
            {
                Id = subClass.Id,
                Name = subClass.Name,
                Description = subClass.Description,
                //SubClass = subClass.SubClass?.AsDto()
            };
        }
        public static CasterSubClassDto AsDto(this CasterSubClass casterSubClass)
        {
            return new CasterSubClassDto
            {
                Id = casterSubClass.Id,
                Name = casterSubClass.Name,
                Description = casterSubClass.Description,
                //SubClass = casterSubClass.SubClass?.AsDto(),
            };
        }
        public static DurationDto AsDto(this Duration duration)
        {
            return new DurationDto
            {
                Amount = duration.Amount,
                Contentration = duration.Contentration,
                Unit = (SpellDurationUnitDto)duration.Unit,
                UpTo = duration.UpTo
            };
        }
        public static ComponentsDto AsDto(this Components components)
        {
            return new ComponentsDto
            {
                Verbal = components.Verbal,
                Somatic = components.Somatic,
                Material = components.Material,
                MaterialComponents = components.MaterialComponents?.Select(materialComponent => materialComponent.AsDto())
            };
        }
        public static MaterialComponentDto AsDto(this MaterialComponent materialComponent)
        {
            return new MaterialComponentDto
            {
                Name = materialComponent.Name,
                Value = materialComponent.Value,
                Consumed = materialComponent.Consumed,
                Description = materialComponent.Description
            };
        }
    }
}
