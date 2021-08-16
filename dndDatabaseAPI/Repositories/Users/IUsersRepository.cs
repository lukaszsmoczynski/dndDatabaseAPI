using dndDatabaseAPI.Models.Users;
using System.Threading.Tasks;

namespace dndDatabaseAPI.Repositories
{
    public interface IUsersRepository : IRepository<User>
    {
        public Task<User> GetByUsernameAsync(string name);
        public Task<User> GetByRefreshTokenAsync(string refreshToken);
    }
}