using System.Linq.Expressions;

namespace CarRental.SharedKernel.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<T> Insert(T entity);
        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> filter = null);
        Task<T> Update(T entity);
        Task<bool> Delete(T entity);
        Task<bool> Delete(Expression<Func<T, bool>> filter = null);
    }
}
