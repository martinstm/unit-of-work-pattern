using System.Collections.Generic;
using System.Threading.Tasks;

namespace EFCore.Core.RepositoryPattern
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        void Update(T entity);
        Task InsertAsync(T entity);
        Task InsertAsync(IEnumerable<T> list);
        void Delete(T entity);
    }
}
