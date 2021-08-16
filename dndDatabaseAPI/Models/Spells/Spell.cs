using dndDatabaseAPI.Models.Characters.Classes;
using dndDatabaseAPI.Models.Dices;
using System;
using System.Collections.Generic;

namespace dndDatabaseAPI.Models.Spells
{
    public record Spell
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public School School { get; set; }
        public CastTime CastTime { get; set; }
        public Range Range { get; set; }
        public Components Components { get; set; }
        public Duration Duration { get; set; }
        public string Description { get; set; }
        public IEnumerable<Roll> Rolls { get; set; }
        public IEnumerable<string> Classes { get; set; }
        public bool Ritual { get; set; }
        public IEnumerable<Upcasting> Upcasting { get; set; }
        //public Stream Image { get; set; }
    }
}
