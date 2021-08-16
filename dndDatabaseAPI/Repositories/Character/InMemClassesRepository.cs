using dndDatabaseAPI.Models.Characters.Classes;
using dndDatabaseAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dndDatabaseAPI.Repositories.Character
{
    public class InMemClassesRepository : IClassesRepository
    {
        private readonly IList<Class> classes = new List<Class>();

        public async Task<IEnumerable<Class>> GetAllAsync()
        {
            return await Task.FromResult(classes);
        }

        public async Task<Class> GetAsync(Guid id)
        {
            return await Task.FromResult(classes.Where(_class => _class.Id.Equals(id)).SingleOrDefault());
        }

        public async Task CreateAsync(Class t)
        {
            classes.Add(t);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Class t)
        {
            var existingSpell = classes.Where(_class => _class.Id.Equals(t.Id)).SingleOrDefault();
            if (existingSpell is null)
            {
                //throw NotFound();
                return;
            }
            classes.Remove(existingSpell);
            classes.Add(t);

            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Class t)
        {
            var existingSpell = classes.Where(_class => _class.Id.Equals(t.Id)).SingleOrDefault();
            if (existingSpell is null)
            {
                //throw NotFound();
                return;
            }
            classes.Remove(existingSpell);

            await Task.CompletedTask;
        }
    }
}
