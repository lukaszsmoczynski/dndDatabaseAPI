using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dndDatabaseAPI.Models.Characters.Classes
{
    public class CharacterClass : ICharacterClass
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
