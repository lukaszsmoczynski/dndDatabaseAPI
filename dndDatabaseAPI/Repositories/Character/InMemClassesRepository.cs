using dndDatabaseAPI.Models.Characters.Classes;
using dndDatabaseAPI.Models.Spells;
using dndDatabaseAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dndDatabaseAPI.Character.Repositories
{
    public class InMemClassesRepository : IRepository<ICharacterClass>
    {
        private readonly List<CharacterClass> characterClass = new();

        public Task<IEnumerable<ICharacterClass>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ICharacterClass> GetAsync(Guid id)
        {
            return await Task.FromResult(characterClass.Where(characterClass => characterClass.Id.Equals(id)).SingleOrDefault()); 
            //return characterClass.Where(characterClass => characterClass.Name.Equals(id)).SingleOrDefault();
        }

        public Task CreateAsync(ICharacterClass t)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ICharacterClass t)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(ICharacterClass t)
        {
            throw new NotImplementedException();
        }
    }
}
