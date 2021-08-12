using dndDatabaseAPI.Models.Characters.Classes;
using dndDatabaseAPI.Models.Dices;
using System;
using System.Collections.Generic;
using System.IO;

namespace dndDatabaseAPI.Models.Spells
{
    public record Spell
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public School School { get; set; }
        public CastTime CastTime { get; set; } = new CastTime();
        public Range Range { get; set; } = new Range();
        public Components Components { get; set; } = new Components();
        public Duration Duration { get; set; } = new Duration();
        public string Description { get; set; }
        public List<Roll> Rolls { get; set; } = new List<Roll>();
        public List<ICasterClass> Classes { get; set; } = new List<ICasterClass>();
        public bool Ritual { get; set; }
        public List<Upcasting> Upcasting { get; set; } = new List<Upcasting>();
        //public Stream Image { get; set; }
    }
}
