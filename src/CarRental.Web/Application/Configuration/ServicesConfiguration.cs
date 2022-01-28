using CarRental.Core.Domain.RepositoryInterfaces;
using CarRental.Infrastructure.Data.Database;
using CarRental.Infrastructure.Data.Repositories;
using CarRental.SharedKernel.Repository;
using CarRental.SharedKernel.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Web.Application.Configuration
{
    public static class ServicesConfiguration
    {
        public static void SetupServices(this IServiceCollection services, IConfiguration configuration)
        {
            SetupDatabase<CarRentalDbContext>(services, configuration, "CarRentalPostgreSQL");
            SetupRepositories(services);
            SetupUnitOfWork(services);
        }

        private static void SetupDatabase<TContext>(IServiceCollection services, IConfiguration configuration, string connectionStringName) where TContext : DbContext
        {
            services.AddDbContext<TContext>(options => options.UseNpgsql(configuration.GetConnectionString(connectionStringName)));
        }

        private static void SetupRepositories(IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            // Add repository implementations here
            services.AddTransient<ICustomerRepository, CustomerRepository>();
        }

        private static void SetupUnitOfWork(IServiceCollection services)
        {
            services.AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        }
    }
}
