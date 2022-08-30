using CarRental.SharedKernel.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.Shared.Service
{
    public class Service<TContext> : IService<TContext> where TContext : DbContext
    {
        protected IUnitOfWork<TContext> UoW { get; }

        public Service(IUnitOfWork<TContext> uoW)
        {
            UoW = uoW;
        }
    }
}
