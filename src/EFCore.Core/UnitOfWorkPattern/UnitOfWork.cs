using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EFCore.Core.UnitOfWorkPattern
{
    public class UnitOfWork<T> : IUnitOfWork, IDisposable where T : DbContext
    {
        private T _dbContext;
        private bool _disposed;

        public UnitOfWork(T dbContext)
        {
            _dbContext = dbContext;
        }
        public DbContext DbContext => _dbContext;

        public async Task BeginTransactionAsync()
        {
            await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await _dbContext.Database.CommitTransactionAsync();
        }

        public async Task RollbackAsync()
        {
            await _dbContext.Database.RollbackTransactionAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    try
                    {
                        _dbContext.Dispose();
                        _dbContext = null;
                    }
                    catch (ObjectDisposedException)
                    {
                        //the object has already be disposed
                    }
                    _disposed = true;
                }
            }
        }
    }
}
