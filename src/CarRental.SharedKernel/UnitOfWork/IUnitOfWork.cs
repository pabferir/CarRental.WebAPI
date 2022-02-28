using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CarRental.SharedKernel.UnitOfWork
{
    public interface IUnitOfWork<TContext> where TContext : DbContext
    {
        /// <summary>
        /// Retrieves a Repository from the service container.
        /// </summary>
        /// <typeparam name="TRepository"> The type of the Repository. </typeparam>
        /// <returns> A service object of type TRepository. </returns>
        TRepository GetRepository<TRepository>() where TRepository : class;

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

        /// <summary>
        /// Asynchronously starts a new DbContext transaction.
        /// </summary>
        /// <returns> A task that contains a IDbContextTransaction representing the transaction. </returns>
        Task<IDbContextTransaction> BeginTransactionAsync();

        /// <summary>
        /// Asynchronously commits to the Database all changes made in the context TContext within a given DbContext transaction.
        /// </summary>
        /// <param name="transaction"> Represents the ongoing transaction to commit. </param>
        /// <returns> A task representing the asynchronous operation. </returns>
        Task CommitAsync(IDbContextTransaction transaction);

        /// <summary>
        /// Asynchronously discards all changes made in the context TContext within a given DbContext transaction.
        /// </summary>
        /// <param name="transaction"> Represents the ongoing transaction to rollback. </param>
        /// <returns> A task representing the asynchronous operation. </returns>
        Task RollbackAsync(IDbContextTransaction transaction);
    }
}
