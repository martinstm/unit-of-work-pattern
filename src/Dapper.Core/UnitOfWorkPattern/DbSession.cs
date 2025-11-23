using Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Data;

namespace Dapper.Core.UnitOfWorkPattern
{
    public sealed class DbSession : IDisposable
    {
        private Guid _id;
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }

        public DbSession(IOptions<DatabaseOptions> options)
        {
            _id = Guid.NewGuid();
            Connection = new SqlConnection(options.Value.ConnectionString);
            Connection.Open();
        }

        public void Dispose() => Connection?.Dispose();
    }
}
