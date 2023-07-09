using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Framework.Common.Interfaces
{

    //by using this IGenericUnitOfwork you can definde multiple unit of work for with multiple sql database
    public interface IGenericUnitOfwork<TContext>
    {
        int SaveChanges(string currentUserID);
        Task<int> SaveChangesAsync(string currentUserID,bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(string currentUserID,CancellationToken cancellationToken = default);
        void BeginTransaction();
        void Commit();
        void RollBack();
    }
}
