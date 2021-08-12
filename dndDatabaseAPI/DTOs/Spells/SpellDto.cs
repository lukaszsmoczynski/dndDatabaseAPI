using dndDatabaseAPI.DTOs.Dices;
using dndDatabaseAPI.Models.Characters.Classes;
using dndDatabaseAPI.Models.Dices;
using dndDatabaseAPI.Models.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dndDatabaseAPI.DTOs.Spells
{
    public class SpellDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public School School { get; set; }
        public CastTimeDto CastTime { get; set; } = new CastTimeDto();
        public RangeDto Range { get; set; } = new RangeDto();
        public Components Components { get; set; } = new Components();
        public Duration Duration { get; set; } = new Duration();
        public string Description { get; set; }
        public List<RollDto> Rolls { get; set; } = new List<RollDto>();
        public List<ICasterClass> Classes { get; set; } = new List<ICasterClass>();
        public bool Ritual { get; set; }
        public List<Upcasting> Upcasting { get; set; } = new List<Upcasting>();
    }
}
