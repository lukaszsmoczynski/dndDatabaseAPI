using dndDatabaseAPI.Models.Users;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dndDatabaseAPI.Repositories.Users
{
    public class MongoDbUsersRepository : IUsersRepository
    {
        private const string databaseName = "dndDatabase";
        private const string collectionName = "users";

        private readonly IMongoCollection<User> usersCollection;
        private readonly FilterDefinitionBuilder<User> filterBuilder = Builders<User>.Filter;

        public MongoDbUsersRepository(IMongoClient client)
        {
            IMongoDatabase database = client.GetDatabase(databaseName);
            usersCollection = database.GetCollection<User>(collectionName);
        }

        public async Task CreateAsync(User t)
        {
            await usersCollection.InsertOneAsync(t);
        }

        public async Task DeleteAsync(User t)
        {
            var filter = filterBuilder.Eq(user => user.Id, t.Id);
            await usersCollection.DeleteOneAsync(filter);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await usersCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<User> GetAsync(Guid id)
        {
            var filter = filterBuilder.Eq(user => user.Id, id);
            return await usersCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<User> GetByRefreshTokenAsync(string refreshToken)
        {
            var filter = filterBuilder.ElemMatch(user => user.RefreshTokens, token => token.Token.Equals(refreshToken));
            return await usersCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            var filter = filterBuilder.Eq(user => user.Username, username);
            return await usersCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task UpdateAsync(User t)
        {
            var filter = filterBuilder.Eq(user => user.Id, t.Id);
            await usersCollection.ReplaceOneAsync(filter, t);
        }
    }
}
