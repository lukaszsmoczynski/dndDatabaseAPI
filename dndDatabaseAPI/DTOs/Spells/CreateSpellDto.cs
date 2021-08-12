using dndDatabaseAPI.DTOs.Dices;
using dndDatabaseAPI.Models.Characters.Classes;
using dndDatabaseAPI.Models.Spells;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace dndDatabaseAPI.DTOs.Spells
{
    public class CreateSpellDto
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public School School { get; set; }
        public CastTimeDto CastTime { get; set; } = new CastTimeDto();
        public RangeDto Range { get; set; } = new RangeDto();
        public ComponentsDto Components { get; set; } = new ComponentsDto();
        public DurationDto Duration { get; set; } = new DurationDto();
        public string Description { get; set; }
        public List<RollDto> Rolls { get; set; } = new List<RollDto>();
        public List<ICasterClass> Classes { get; set; } = new List<ICasterClass>();
        public bool Ritual { get; set; }
        public List<Upcasting> Upcasting { get; set; } = new List<Upcasting>();
    }
}
