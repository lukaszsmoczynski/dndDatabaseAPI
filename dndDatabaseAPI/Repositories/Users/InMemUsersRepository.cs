using dndDatabaseAPI.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dndDatabaseAPI.Repositories.Users
{
    public class InMemUsersRepository : IUsersRepository
    {
        private readonly IList<User> Users = new List<User>();

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await Task.FromResult(Users);
        }

        public async Task<User> GetAsync(Guid id)
        {
            return await Task.FromResult(Users.Where(User => User.Id.Equals(id)).SingleOrDefault());
        }

        public async Task CreateAsync(User t)
        {
            Users.Add(t);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(User t)
        {
            var existingUser = Users.Where(User => User.Id.Equals(t.Id)).SingleOrDefault();
            if (existingUser is null)
            {
                //throw NotFound();
                return;
            }
            Users.Remove(existingUser);
            Users.Add(t);

            await Task.CompletedTask;
        }

        public async Task DeleteAsync(User t)
        {
            var existingUser = Users.Where(User => User.Id.Equals(t.Id)).SingleOrDefault();
            if (existingUser is null)
            {
                //throw NotFound();
                return;
            }
            Users.Remove(existingUser);

            await Task.CompletedTask;
        }

        public async Task<User> GetByUsernameAsync(string name)
        {
            return await Task.FromResult(Users.Where(User => User.Username.Equals(name)).SingleOrDefault());
        }

        public async Task<User> GetByRefreshTokenAsync(string refreshToken)
        {
            return await Task.FromResult(Users.Where(User => User.RefreshTokens.Any(token => token.Token.Equals(refreshToken))).SingleOrDefault());
        }
    }
}
