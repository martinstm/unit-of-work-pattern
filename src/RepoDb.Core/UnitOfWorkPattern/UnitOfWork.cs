using Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Data;

namespace RepoDb.Core.UnitOfWorkPattern
{
    // reference: https://repodb.net/reference/output/unitofwork
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private SqlConnection _connection;
        private IDbTransaction _transaction;
        private string _connectionString;

        public UnitOfWork(IOptions<DatabaseOptions> options)
        {
            _connectionString = options.Value.ConnectionString;
        }

        public SqlConnection Connection => _connection;

        public IDbTransaction Transaction => _transaction;

        public void BeginTransaction()
        {
            if (_transaction != null)
            {
                throw new InvalidOperationException("Cannot start a new transaction while the existing one is still open.");
            }
            if (_connection == null)
                _connection = new SqlConnection(_connectionString);

            _transaction = _connection.EnsureOpen().BeginTransaction();
        }

        public void Commit()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("There is no active transaction to commit.");
            }
            using (_transaction)
            {
                _transaction.Commit();
            }
            _transaction = null;
        }

        public void Rollback()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("There is no active transaction to rollback.");
            }
            using (_transaction)
            {
                _transaction.Rollback();
            }
            _transaction = null;
        }

        public void Dispose()
        {
            _transaction?.Dispose();
        }
    }
}
