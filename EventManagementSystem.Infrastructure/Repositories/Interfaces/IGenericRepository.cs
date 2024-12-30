namespace EventManagementSystem.Infrastructure.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        // Task SaveChangesAsync(); // WE use SaveChanges from unitofwork class.

        Task<IEnumerable<TEntity>> GetPaginatedAsync(int pageIndex, int pageSize, Func<IQueryable<TEntity>, IQueryable<TEntity>>? queryModifier = null);
    }
}