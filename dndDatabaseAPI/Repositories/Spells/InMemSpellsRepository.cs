using dndDatabaseAPI.Models.Spells;
using dndDatabaseAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dndDatabaseAPI.Spells.Repositories
{
    public class InMemSpellsRepository : ISpellsRepository
    {
        private readonly IList<Spell> spells = new List<Spell>();

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
            spells.Add(t);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Spell t)
        {
            var existingSpell = spells.Where(spell => spell.Id.Equals(t.Id)).SingleOrDefault();
            if (existingSpell is null)
            {
                //throw NotFound();
                return;
            }
            spells.Remove(existingSpell);
            spells.Add(t);

            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Spell t)
        {
            var existingSpell = spells.Where(spell => spell.Id.Equals(t.Id)).SingleOrDefault();
            if (existingSpell is null)
            {
                //throw NotFound();
                return;
            }
            spells.Remove(existingSpell);

            await Task.CompletedTask;
        }
    }
}
