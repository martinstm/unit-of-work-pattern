using Dapper.Core.RepositoryPattern;
using System.Data;

namespace Dapper.Core.UnitOfWorkPattern
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbSession _session;
        private readonly IRepositoryFactory _repositoryFactory;

        public UnitOfWork(DbSession session, IRepositoryFactory repositoryFactory)
        {
            _session = session;
            _repositoryFactory = repositoryFactory;
        }

        public IDbConnection Connection => _session.Connection;
        public IDbTransaction Transaction => _session.Transaction;

        public TRepository Get<TRepository>() where TRepository : class
        {
            return _repositoryFactory.GetRepository<TRepository>();
        }

        public void BeginTransaction()
        {
            _session.Transaction = _session.Connection.BeginTransaction();
        }

        public void Commit()
        {
            _session.Transaction.Commit();
            Dispose();
        }

        public void Rollback()
        {
            _session.Transaction.Rollback();
            Dispose();
        }

        public void Dispose() => _session.Transaction?.Dispose();
    }
}
