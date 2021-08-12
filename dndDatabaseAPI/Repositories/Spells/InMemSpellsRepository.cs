using dndDatabaseAPI.Models.Spells;
using dndDatabaseAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dndDatabaseAPI.Spells.Repositories
{
    public class InMemSpellsRepository : IRepository<Spell>
    {
        private readonly List<Spell> spells = new();

        public async Task<IEnumerable<Spell>> GetAllAsync()
        {
            return await Task.FromResult(spells);
        }

        public async Task<Spell> GetAsync(Guid id)
        {
            return await Task.FromResult(spells.Where(spell => spell.Id.Equals(id)).SingleOrDefault());
        }

        public async Task CreateAsync(Spell t)
        {
            //throw new NotImplementedException();
            await Task.CompletedTask;
        }

        public Task UpdateAsync(Spell t)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Spell t)
        {
            throw new NotImplementedException();
        }
    }
}
