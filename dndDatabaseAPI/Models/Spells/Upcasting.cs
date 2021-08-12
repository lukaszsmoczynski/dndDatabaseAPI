using dndDatabaseAPI.Models.Dices;
using System.Collections.Generic;

namespace dndDatabaseAPI.Models.Spells
{
    public class Upcasting
    {
        public int Level { get; set; }
        public string Description { get; set; }
        public List<Roll> Rolls { get; set; } = new List<Roll>();
    }
}