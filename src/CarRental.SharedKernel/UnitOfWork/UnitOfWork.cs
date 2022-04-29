using CarRental.SharedKernel.UnitOfWork.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.SharedKernel.UnitOfWork
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
    {
        private TContext Context { get; }
        private IServiceProvider ServiceProvider { get; }

        public UnitOfWork(TContext context, IServiceProvider serviceProvider)
        {
            Context = context;
            ServiceProvider = serviceProvider;
        }

        /// <summary>
        /// Retrieves a Repository from the service container.
        /// </summary>
        /// <typeparam name="TRepository"> The type of the Repository. </typeparam>
        /// <returns> A service object of type TRepository. </returns>
        /// <exception cref="RepositoryNotFoundException"> Thrown when the requested Repository is not found in the service container. </exception>
        public TRepository GetRepository<TRepository>() where TRepository : class
        {
            var repository = ServiceProvider.GetService<TRepository>();
            if (repository == null)
            {
                throw new RepositoryNotFoundException($"Couldn't find the repository of type {typeof(TRepository).Name} in the services container. Please, register the repository during startup.");
            }

            return repository;
        }

        /// <summary>
        /// Asynchronously starts a new DbContext transaction.
        /// </summary>
        /// <returns> A task that contains a IDbContextTransaction representing the transaction. </returns>
        public Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return Context.Database.BeginTransactionAsync();
        }

        /// <summary>
        /// Asynchronously commits to the Database all changes made in the context TContext within a given DbContext transaction.
        /// </summary>
        /// <param name="transaction"> Represents the ongoing transaction to commit. </param>
        /// <returns> A task representing the asynchronous operation. </returns>
        /// <exception cref="NullTransactionException"> Thrown when the transaction provided is not currently available. </exception>
        public async Task CommitAsync(IDbContextTransaction transaction)
        {
            if (transaction == null)
            {
                throw new NullTransactionException("Cannot commit database operation from null transaction.");
            }
            try
            {
                await SaveChangesAsync(transaction).ConfigureAwait(false);
                await transaction.CommitAsync().ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                await RollbackAsync(transaction).ConfigureAwait(false);
                throw;
            }
        }

        /// <summary>
        /// Asynchronously attempts to save all changes made in the context TContext to the Database.
        /// </summary>
        /// <param name="transaction"> Represents the ongoing transaction where trying to save changes. </param>
        /// <returns> The number of state entities written to database. </returns>
        /// <exception cref="DbUpdateConcurrencyException"> Thrown when concurrency conflicts occur during the operation.</exception>
        /// More info on how to handle concurrency conflicts: https://docs.microsoft.com/en-us/ef/core/saving/concurrency
        private async Task<int> SaveChangesAsync(IDbContextTransaction transaction)
        {
            try
            {
                int result = await Context.SaveChangesAsync().ConfigureAwait(false);
                DetachTrackedEntities();
                return result;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                foreach (var entry in ex.Entries)
                {
                    var databaseValues = entry.GetDatabaseValues();
                    entry.OriginalValues.SetValues(databaseValues);
                }
                await RollbackAsync(transaction).ConfigureAwait(false);
                throw;
            }
        }

        /// <summary>
        /// Asynchronously discards all changes made in the context TContext within a given DbContext transaction.
        /// </summary>
        /// <param name="transaction"> Represents the ongoing transaction to rollback. </param>
        /// <returns> A task representing the asynchronous operation. </returns>
        /// <exception cref="NullTransactionException"> Thrown when the transaction provided is not currently available. </exception>
        private static Task RollbackAsync(IDbContextTransaction transaction)
        {
            if (transaction == null)
            {
                throw new NullTransactionException("Cannot rollback database operation from null transaction.");
            }

            return transaction.RollbackAsync();
        }

        /// <summary>
        /// Sets as detached all the entities being tracked in the TContext context.
        /// </summary>
        private void DetachTrackedEntities()
        {
            var trackedEntities = Context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted)
                .ToList();

            foreach (var entity in trackedEntities)
            {
                entity.State = EntityState.Detached;
            }
        }
    }
}
