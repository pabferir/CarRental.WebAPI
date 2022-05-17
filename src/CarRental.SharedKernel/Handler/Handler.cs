using CarRental.SharedKernel.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace CarRental.SharedKernel.Handler
{
    public class Handler<TContext> : IHandler<TContext> where TContext : DbContext
    {
        protected IUnitOfWork<TContext> UoW { get; }

        public Handler(IUnitOfWork<TContext> uoW)
        {
            UoW = uoW;
        }
    }
}
