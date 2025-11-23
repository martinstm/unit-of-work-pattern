using Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepoDb.Core.RepositoryPattern
{
    public abstract class Repository<T> : BaseRepository<T, SqlConnection>, IRepository<T> where T : TableEntity
    {
        protected Repository(IOptions<DatabaseOptions> options) : base(options.Value.ConnectionString)
        {

        }

        public async Task InsertAsync(T entity)
        {
            await base.InsertAsync(entity);
        }

        public async Task InsertAsync(IEnumerable<T> entities)
        {
            await InsertAllAsync(entities);
        }

        public async Task UpdateAsync(T entity)
        {
            await base.UpdateAsync(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            await base.DeleteAsync(entity);
        }
    }
}
