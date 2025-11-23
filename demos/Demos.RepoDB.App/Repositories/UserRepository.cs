using Configuration;
using Demos.RepoDB.App.Domain;
using Microsoft.Extensions.Options;
using RepoDb.Core.RepositoryPattern;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demos.RepoDB.App.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IOptions<DatabaseOptions> options) : base(options)
        {
        }

        public async Task<IReadOnlyCollection<User>> GetAllAsync()
        {
            return (await QueryAllAsync()).ToArray();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return (await QueryAsync(x => x.Email == email)).SingleOrDefault();
        }
    }
}
