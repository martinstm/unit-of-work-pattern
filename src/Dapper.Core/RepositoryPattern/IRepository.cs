using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dapper.Core.RepositoryPattern
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(Guid id);
        Task UpdateAsync(T t);
        Task InsertAsync(T t);
        Task<int> InsertAsync(IEnumerable<T> list);
        Task DeleteAsync(Guid id);
    }
}
