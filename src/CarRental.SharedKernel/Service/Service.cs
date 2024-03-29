﻿using CarRental.SharedKernel.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace CarRental.SharedKernel.Service
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
