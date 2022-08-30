using CarRental.SharedKernel.Repository.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CarRental.Infrastructure.Shared.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DbContext DbContext { get; }

        public Repository(DbContext context)
        {
            DbContext = context;
        }

        public async Task<TEntity> Insert(TEntity entity, bool saveChanges = false)
        {
            var result = await DbContext.Set<TEntity>().AddAsync(entity).ConfigureAwait(false);
            result.State = EntityState.Added;
            if (saveChanges)
            {
                await DbContext.SaveChangesAsync().ConfigureAwait(false);
                result.State = EntityState.Detached;
            }
            return result.Entity;
        }

        public async Task<IEnumerable<TEntity>> GetWhere(Expression<Func<TEntity, bool>> predicate = null)
        {
            IQueryable<TEntity> query = DbContext.Set<TEntity>().AsNoTracking();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return await query.ToListAsync().ConfigureAwait(false);
        }

        public async Task<TEntity> Update(TEntity entity, bool saveChanges = false)
        {
            var result = DbContext.Set<TEntity>().Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
            if (saveChanges)
            {
                await DbContext.SaveChangesAsync().ConfigureAwait(false);
                result.State = EntityState.Detached;
            }
            return result.Entity;
        }

        public async Task<TEntity> Delete(TEntity entity, bool saveChanges = false)
        {
            var result = DbContext.Set<TEntity>().Remove(entity);
            if (result == null)
            {
                throw new DatabaseOperationNotCompletedException($"Couldn't delete the entity { entity } in the Database.");
            }
            result.State = EntityState.Deleted;
            if (saveChanges)
            {
                await DbContext.SaveChangesAsync().ConfigureAwait(false);
                result.State = EntityState.Detached;
            }
            return result.Entity;
        }

        public async Task<IEnumerable<TEntity>> DeleteWhere(Expression<Func<TEntity, bool>> predicate = null, bool saveChanges = false)
        {
            var result = new List<TEntity>();
            var entities = await GetWhere(predicate).ConfigureAwait(false);
            if (!entities.Any())
            {
                return result;
            }
            foreach (var entity in entities)
            {
                result.Add(await Delete(entity, saveChanges).ConfigureAwait(false));
            }
            return result;
        }
    }
}
