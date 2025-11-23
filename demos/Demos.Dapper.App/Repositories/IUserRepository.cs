using Dapper.Core.RepositoryPattern;
using Demos.Dapper.App.Domain;
using System.Threading.Tasks;

namespace Demos.Dapper.App.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
    }
}
