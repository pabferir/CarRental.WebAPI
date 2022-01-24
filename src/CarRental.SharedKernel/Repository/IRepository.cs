using System.Linq.Expressions;

namespace CarRental.SharedKernel.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Insert(TEntity entity);
        Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> filter);
        Task<TEntity> Update(TEntity entity);
        Task<bool> Delete(TEntity entity);
        Task<bool> Delete(Expression<Func<TEntity, bool>> filter);
    }
}
