using dndDatabaseAPI.Models.Spells;
using dndDatabaseAPI.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dndDatabaseAPI.Spells.Repositories
{
    public class MongoDbSpellsRepository : ISpellsRepository
    {
        private const string databaseName = "dndDatabase";
        private const string collectionName = "spells";

        private readonly IMongoCollection<Spell> spellsCollection;
        private readonly FilterDefinitionBuilder<Spell> filterBuilder = Builders<Spell>.Filter;
        public MongoDbSpellsRepository(IMongoClient client)
        {
            IMongoDatabase database = client.GetDatabase(databaseName);
            spellsCollection = database.GetCollection<Spell>(collectionName);
        }
        public async Task<IEnumerable<Spell>> GetAllAsync()
        {
            return await spellsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Spell> GetAsync(Guid id)
        {
            var filter = filterBuilder.Eq(spell => spell.Id, id);
            return await spellsCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task CreateAsync(Spell t)
        {
            await spellsCollection.InsertOneAsync(t);
        }

        public async Task UpdateAsync(Spell t)
        {
            var filter = filterBuilder.Eq(spell => spell.Id, t.Id);
            await spellsCollection.ReplaceOneAsync(filter, t);
        }

        public async Task DeleteAsync(Spell t)
        {
            var filter = filterBuilder.Eq(spell => spell.Id, t.Id);
            await spellsCollection.DeleteOneAsync(filter);
        }
    }
}
