using CarRental.SharedKernel.Exceptions;
using CarRental.SharedKernel.Repository;
using Microsoft.EntityFrameworkCore;
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
        /// <exception cref="RepositoryNotFoundException"> The requested Repository was not found in the service container. </exception>
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
        /// Saves all changes made in the context TContext to the Database.
        /// </summary>
        /// <returns> The number of state entities written to database. </returns>
        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        /// <summary>
        /// Asynchronously saves all changes made in the context TContext to the Database.
        /// </summary>
        /// <returns> The number of state entities written to database. </returns>
        public Task<int> SaveChangesAsync()
        {
            return Context.SaveChangesAsync();
        }
    }
}
