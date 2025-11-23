using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepoDb.Core.RepositoryPattern
{
    public interface IRepository<T> where T : class
    {
        Task InsertAsync(T entity);
        Task InsertAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
