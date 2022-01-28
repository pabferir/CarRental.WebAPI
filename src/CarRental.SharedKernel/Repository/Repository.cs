using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CarRental.SharedKernel.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DbContext DbContext { get; }

        public Repository(DbContext context)
        {
            DbContext = context;
        }

        public async Task<TEntity> Insert(TEntity entity, bool saveChanges = true)
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

        public async Task<IEnumerable<TEntity>> GetWhere(Expression<Func<TEntity, bool>>? filter = null)
        {
            IQueryable<TEntity> query = DbContext.Set<TEntity>().AsNoTracking();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync().ConfigureAwait(false);
        }

        public async Task<TEntity> Update(TEntity entity, bool saveChanges = true)
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

        public async Task<bool> Delete(TEntity entity, bool saveChanges = true)
        {
            var result = DbContext.Set<TEntity>().Remove(entity);
            result.State = EntityState.Deleted;
            var deleted = 0;
            if (saveChanges)
            {
                deleted = await DbContext.SaveChangesAsync().ConfigureAwait(false);
                result.State = EntityState.Detached;
            }
            return deleted > 0;
        }

        public async Task<bool> DeleteWhere(Expression<Func<TEntity, bool>>? filter = null, bool saveChanges = true)
        {
            var result = new List<bool>();
            var entities = await GetWhere(filter).ConfigureAwait(false);
            foreach(var entity in entities)
            {
                result.Add(await Delete(entity, saveChanges).ConfigureAwait(false));
            }
            return result.All(r => r == true);
        }
    }
}
