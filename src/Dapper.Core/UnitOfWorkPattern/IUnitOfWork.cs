using System.Data;

namespace Dapper.Core.UnitOfWorkPattern
{
    public interface IUnitOfWork
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
        void BeginTransaction();
        void Commit();
        void Rollback();
        TRepository Get<TRepository>() where TRepository : class;
    }
}
