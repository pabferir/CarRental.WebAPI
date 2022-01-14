using CarRental.Infrastructure.Data.Database;
using CarRental.SharedKernel.Repository;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Web.Application.Configuration
{
    public static class ServicesConfiguration
    {
        public static void SetupServices(this IServiceCollection services, IConfiguration configuration)
        {
            SetupDatabase<CarRentalDbContext>(services, configuration, "CarRentalPostgreSQL");
            SetupRepositories(services);
        }

        private static void SetupDatabase<T>(IServiceCollection services, IConfiguration configuration, string connectionStringName) where T : DbContext
        {
            services.AddDbContext<T>(options => options.UseNpgsql(configuration.GetConnectionString(connectionStringName)));
        }

        private static void SetupRepositories(IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
