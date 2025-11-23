using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading.Tasks;

namespace EFCore.Core.UnitOfWorkPattern
{
    public interface IUnitOfWork
    {
        DbContext DbContext { get; }
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
        Task<int> SaveChangesAsync();
    }
}
