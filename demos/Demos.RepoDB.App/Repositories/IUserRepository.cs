using Demos.RepoDB.App.Domain;
using RepoDb.Core.RepositoryPattern;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demos.RepoDB.App.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IReadOnlyCollection<User>> GetAllAsync();
        Task<User> GetByEmailAsync(string email);
    }
}
