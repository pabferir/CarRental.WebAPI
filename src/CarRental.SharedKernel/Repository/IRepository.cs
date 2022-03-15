using System.Linq.Expressions;

namespace CarRental.SharedKernel.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Insert(TEntity entity, bool saveChanges);
        Task<IEnumerable<TEntity>> GetWhere(Expression<Func<TEntity, bool>> filter);
        Task<TEntity> Update(TEntity entity, bool saveChanges);
        Task<bool> Delete(TEntity entity, bool saveChanges);
        Task<bool> DeleteWhere(Expression<Func<TEntity, bool>> filter, bool saveChanges);
    }
}
