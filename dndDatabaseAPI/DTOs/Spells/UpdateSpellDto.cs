using dndDatabaseAPI.DTOs.Dices;
using dndDatabaseAPI.Models.Characters.Classes;
using dndDatabaseAPI.Models.Spells;
using System.Collections.Generic;

namespace dndDatabaseAPI.DTOs.Spells
{
    public record UpdateSpellDto
    {
        public string Name { get; init; }
        public int Level { get; init; }
        public School School { get; init; }
        public CastTimeDto CastTime { get; init; }
        public RangeDto Range { get; init; }
        public ComponentsDto Components { get; init; }
        public DurationDto Duration { get; init; }
        public string Description { get; init; }
        public IEnumerable<RollDto> Rolls { get; init; }
        public IEnumerable<CasterClass> Classes { get; init; }
        public bool Ritual { get; init; }
        public IEnumerable<Upcasting> Upcasting { get; init; }
    }
}
