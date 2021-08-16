using dndDatabaseAPI.Models.Spells;
using dndDatabaseAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dndDatabaseAPI.Spells.Repositories
{
    public class PostgreSpellsRepository : ISpellsRepository
    {

        public Task<IEnumerable<Spell>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Spell> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(Spell t)
        {
            throw new NotImplementedException();
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
