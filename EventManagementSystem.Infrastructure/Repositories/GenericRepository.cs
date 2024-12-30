using EventManagementSystem.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementSystem.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly EventManagementDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(EventManagementDbContext dbContext) { 
            _dbContext= dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
           await _dbContext.Set<TEntity>().AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
             _dbContext.Set<TEntity>().Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetPaginatedAsync(int pageIndex, int pageSize, Func<IQueryable<TEntity>, IQueryable<TEntity>>? queryModifier = null)
        {
            IQueryable<TEntity> query = _dbSet;

            // Apply query modification if provided (e.g., filtering, sorting)
            if(queryModifier != null)
            {
                query = queryModifier(query);
            }

            return await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        // WE use SaveChanges from unit of work class.

        //public Task SaveChangesAsync()
        //{
        //    throw new NotImplementedException();
        //}

        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }
    }
}
