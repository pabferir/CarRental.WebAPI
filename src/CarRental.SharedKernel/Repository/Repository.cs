using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.ComponentModel;

namespace CarRental.SharedKernel.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DbContext DbContext { get; set; }

        public Repository(DbContext context)
        {
            DbContext = context;
        }

        public async Task<T> Insert(T entity)
        {
            var result = await DbContext.Set<T>().AddAsync(entity).ConfigureAwait(false);
            result.State = EntityState.Added;
            await DbContext.SaveChangesAsync().ConfigureAwait(false);
            result.State = EntityState.Detached;
            return result.Entity;
        }

        public async Task<IEnumerable<T>> Get(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = DbContext.Set<T>().AsNoTracking();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync().ConfigureAwait(false);
        }

        public async Task<T> Update(T entity)
        {
            var result = DbContext.Set<T>().Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
            await DbContext.SaveChangesAsync().ConfigureAwait(false);
            result.State = EntityState.Detached;
            return result.Entity;
        }

        public async Task<bool> Delete(T entity)
        {
            var result = DbContext.Set<T>().Remove(entity);
            result.State = EntityState.Deleted;
            var deleted = await DbContext.SaveChangesAsync().ConfigureAwait(false);
            result.State = EntityState.Detached;
            return deleted > 0;
        }

        public async Task<bool> Delete(Expression<Func<T, bool>> filter = null)
        {
            var result = new List<bool>();
            var entities = await Get(filter).ConfigureAwait(false);
            foreach(var entity in entities)
            {
                result.Add(await Delete(entity).ConfigureAwait(false));
            }
            return result.All(r => r == true);
        }
    }
}
