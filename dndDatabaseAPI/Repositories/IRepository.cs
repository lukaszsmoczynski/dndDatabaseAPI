using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dndDatabaseAPI.Repositories
{
    public interface IRepository<T>
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> GetAsync(Guid id);
        public Task CreateAsync(T t);
        public Task UpdateAsync(T t);
        public Task DeleteAsync(T t);
    }
}