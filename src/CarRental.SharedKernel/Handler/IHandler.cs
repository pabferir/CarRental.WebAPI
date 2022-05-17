using Microsoft.EntityFrameworkCore;

namespace CarRental.SharedKernel.Handler
{
    public interface IHandler<TContext> where TContext : DbContext
    {
    }
}
