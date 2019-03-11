using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Services.Security.Common.Interfaces
{
    public interface IUnitOfWork  : IDisposable
    {
        //Transaction Transaction { get; set; }
        DbContext Context { get; }
        void SaveChanges();
        Task<int> SaveChangesAsync();
        void Commit();
        void RollBack();
    }
}
