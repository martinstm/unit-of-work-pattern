namespace Dapper.Core.RepositoryPattern
{
    public interface IRepositoryFactory
    {
        TRepository GetRepository<TRepository>() where TRepository : class;
    }
}
