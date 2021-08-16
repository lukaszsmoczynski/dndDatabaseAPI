using dndDatabaseAPI.Models.Characters.Classes;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dndDatabaseAPI.Repositories.Character
{
    public class MongoDbClassesRepository : IClassesRepository
    {
        private const string databaseName = "dndDatabase";
        private const string collectionName = "classes";

        private readonly IMongoCollection<Class> classesCollection;
        private readonly FilterDefinitionBuilder<Class> filterBuilder = Builders<Class>.Filter;
        public MongoDbClassesRepository(IMongoClient client)
        {
            IMongoDatabase database = client.GetDatabase(databaseName);
            classesCollection = database.GetCollection<Class>(collectionName);
        }
        public async Task<IEnumerable<Class>> GetAllAsync()
        {
            return await classesCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Class> GetAsync(Guid id)
        {
            var filter = filterBuilder.Eq(_spell => _spell.Id, id);
            return await classesCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task CreateAsync(Class t)
        {
            await classesCollection.InsertOneAsync(t);
        }

        public async Task UpdateAsync(Class t)
        {
            var filter = filterBuilder.Eq(spell => spell.Id, t.Id);
            await classesCollection.ReplaceOneAsync(filter, t);
        }

        public async Task DeleteAsync(Class t)
        {
            var filter = filterBuilder.Eq(spell => spell.Id, t.Id);
            await classesCollection.DeleteOneAsync(filter);
        }
    }
}
