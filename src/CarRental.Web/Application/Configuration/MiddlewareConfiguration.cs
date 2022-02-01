using CarRental.Core.Domain.Context;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Web.Application.Configuration
{
    public static class MiddlewareConfiguration
    {
        public static void SetupMiddleware(this IApplicationBuilder app, IServiceCollection services)
        {
            UpdateDatabase<CarRentalDbContext>(services);
        }

        private static void UpdateDatabase<TContext>(IServiceCollection services) where TContext : DbContext
        {
            using ServiceProvider serviceProvider = services.BuildServiceProvider();
            if (serviceProvider == null)
            {
                throw new NullReferenceException($"The service provider { serviceProvider } cannot be null");
            }

            object context = serviceProvider.GetService(typeof(TContext));
            if (context == null)
            {
                throw new NullReferenceException($"The context { context } cannot be null");
            }
            ((TContext)context).Database.Migrate();
        }
    }
}
