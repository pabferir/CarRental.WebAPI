using CarRental.SharedKernel.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace CarRental.SharedKernel.Service
{
    public interface IService<TContext> where TContext : DbContext
    {
    }
}
