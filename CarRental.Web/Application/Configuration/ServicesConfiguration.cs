using CarRental.Infrastructure.Data.Database;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Web.Application.Configuration
{
    public static class ServicesConfiguration
    {
        public static void SetupServices(this IServiceCollection services, IConfiguration configuration)
        {
            SetupDatabase<CarRentalDbContext>(services, configuration, "CarRentalPostgreSQL");
        }

        private static void SetupDatabase<T>(IServiceCollection services, IConfiguration configuration, string connectionStringName) where T : DbContext
        {
            services.AddDbContext<T>(options => options.UseNpgsql(configuration.GetConnectionString(connectionStringName)));
        }
    }
}
