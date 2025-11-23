using Microsoft.Data.SqlClient;
using System.Data;

namespace RepoDb.Core.UnitOfWorkPattern
{
    public interface IUnitOfWork
    {
        SqlConnection Connection { get; }
        IDbTransaction Transaction { get; }
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
