using CarRental.Infrastructure.Business.ServiceInterfaces;
using CarRental.Infrastructure.Business.Services;
using CarRental.Infrastructure.Data.Context;
using CarRental.Infrastructure.Data.Repositories;
using CarRental.Infrastructure.Data.RepositoryInterfaces;
using CarRental.SharedKernel.Repository;
using CarRental.SharedKernel.Service;
using CarRental.SharedKernel.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CarRental.Web.Application.Configuration
{
    public static class ServicesConfiguration
    {
        public static void SetupServiceContainer(this WebApplicationBuilder builder)
        {
            SetupDatabase<CarRentalDbContext>(builder.Services, builder.Configuration, "CarRentalPostgreSQL");
            SetupLogging(builder.Configuration, builder.Host);
            SetupRepositories(builder.Services);
            SetupUnitOfWork(builder.Services);
            SetupServices(builder.Services);
        }

        private static void SetupDatabase<TContext>(IServiceCollection services, IConfiguration configuration, string connectionStringName) where TContext : DbContext
        {
            services.AddDbContext<TContext>(options => options.UseNpgsql(configuration.GetConnectionString(connectionStringName)));
        }

        private static void SetupLogging(IConfiguration configuration, IHostBuilder host)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            host.UseSerilog();
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

        private static void SetupServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IService<>), typeof(Service<>));
            // Add service implementations here
            services.AddTransient<ICustomerService, CustomerService>();
        }
    }
}
