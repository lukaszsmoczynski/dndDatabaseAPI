using dndDatabaseAPI.Models.Spells;
using System;
using System.Collections.Generic;
using System.Text;

namespace dndDatabaseAPI.Models.Characters.Classes
{
    public interface ICharacterClass
    {
        public string Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
    }
}
