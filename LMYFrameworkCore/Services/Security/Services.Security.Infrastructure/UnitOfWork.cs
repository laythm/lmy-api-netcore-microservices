using Framework.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Services.Security.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Services.Security.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; }
        //public Transaction Transaction { get; set; }

        public UnitOfWork(DbContext context)
        {
            Context = context;
            //  Transaction = transaction;
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void RollBack()
        {
            throw new NotImplementedException();
        }

    }
}
