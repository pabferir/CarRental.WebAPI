using CarRental.SharedKernel.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.Shared.Service
{
    public interface IService<TContext> where TContext : DbContext
    {
    }
}
