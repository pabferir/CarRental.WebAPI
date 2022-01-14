using CarRental.Infrastructure.Data.Database;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Web.Application.Configuration
{
    public static class MiddlewareConfiguration
    {
        public static void SetupMiddleware(this IApplicationBuilder app, IServiceCollection services)
        {
            UpdateDatabase<CarRentalDbContext>(services);
        }

        private static void UpdateDatabase<T>(IServiceCollection services) where T : DbContext
        {
            using ServiceProvider serviceProvider = services.BuildServiceProvider();
            if (serviceProvider == null)
            {
                throw new NullReferenceException($"The service provider { serviceProvider } cannot be null");
            }

            object context = serviceProvider.GetService(typeof(T));
            if (context == null)
            {
                throw new NullReferenceException($"The context { context } cannot be null");
            }
            ((T)context).Database.Migrate();
        }
    }
}
