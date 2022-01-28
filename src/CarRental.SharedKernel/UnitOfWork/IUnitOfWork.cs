using CarRental.SharedKernel.Repository;
using Microsoft.EntityFrameworkCore;

namespace CarRental.SharedKernel.UnitOfWork
{
    public interface IUnitOfWork<TContext> where TContext : DbContext
    {
        /// <summary>
        /// Retrieves a Repository from the service container.
        /// </summary>
        /// <typeparam name="TEntity"> The domain Entity that the Repository encapsulates. </typeparam>
        /// <returns> The Repository that encapsulates the domain Entity TEntity. </returns>
        TEntity GetRepository<TEntity>() where TEntity : class;

        /// <summary>
        /// Saves all changes made in the context TContext to the Database.
        /// </summary>
        /// <returns> The number of state entities written to database. </returns>
        int SaveChanges();

        /// <summary>
        /// Asynchronously saves all changes made in the context TContext to the Database.
        /// </summary>
        /// <returns> The number of state entities written to database. </returns>
        Task<int> SaveChangesAsync();
    }
}
