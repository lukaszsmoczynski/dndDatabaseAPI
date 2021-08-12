using dndDatabaseAPI.Models.Spells;
using dndDatabaseAPI.Repositories;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dndDatabaseAPI.Spells.Repositories
{
    public class PostgreSpellsRepository : IRepository<Spell>
    {
        private readonly NpgsqlConnection connection;
        public PostgreSpellsRepository(NpgsqlConnection connection)
        {
            this.connection = connection;
        }

        public Task<IEnumerable<Spell>> GetAllAsync()
        {
            throw new NotImplementedException();

            //var result = new List<Spell>();
            //var cmd = new NpgsqlCommand("SELECT * FROM spells", connection);
            //using (var reader = cmd.ExecuteReader())
            //{
            //    while (reader.Read())
            //    {
            //        result.Add(new Spell()
            //        {
            //            Name = reader["name"].ToString()
            //        });
            //    };
            //}

            //return result;
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
