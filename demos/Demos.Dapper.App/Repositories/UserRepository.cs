using Dapper;
using Dapper.Core.RepositoryPattern;
using Dapper.Core.UnitOfWorkPattern;
using Demos.Dapper.App.Domain;
using System.Threading.Tasks;

namespace Demos.Dapper.App.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override string TableName => "[User]";

        public async Task<User> GetByEmailAsync(string email)
        {
            var sql = @$"select * from {TableName} where Email = @Email";

            var user = await _unitOfWork.Connection.QuerySingleAsync<User>(
                        sql, new { Email = email }, _unitOfWork.Transaction);

            return user;
        }
    }
}
