using dndDatabaseAPI.DTOs.Dices;
using System.Collections.Generic;

namespace dndDatabaseAPI.Models.Spells
{
    public record UpcastingDto
    {
        public int Level { get; init; }
        public string Description { get; init; }
        public IEnumerable<RollDto> Rolls { get; init; }
    }
}