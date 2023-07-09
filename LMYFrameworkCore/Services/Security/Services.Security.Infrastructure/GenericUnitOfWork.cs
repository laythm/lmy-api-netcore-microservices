using Framework.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Services.Security.Infrastructure
{
    public class GenericUnitOfWork<TContext> : IGenericUnitOfwork<TContext> where TContext : DbContext
    {
        public DbContext _dbContext { get; }
        private IDbContextTransaction _transaction;

        public GenericUnitOfWork(TContext dbContext)
        {
            _dbContext = dbContext;
            //  Transaction = transaction;
        }

        public void BeginTransaction()
        {
            _transaction = _dbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            if (_transaction != null)
            {
                _transaction.Commit();
            }
        }

        public void RollBack()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
            }
        }

        public int SaveChanges(string currentUserID)
        {
            setTracks(currentUserID);
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync(string currentUserID, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            setTracks(currentUserID);

            return await _dbContext.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken).ConfigureAwait(false);

        }

        public async Task<int> SaveChangesAsync(string currentUserID, CancellationToken cancellationToken = default)
        {
            setTracks(currentUserID);

            return await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        private void setTracks(string currentUserID)
        {
            var entries = _dbContext.ChangeTracker.Entries().Where(x =>
            x.Entity is ITrackable || x.Entity is IUserTrackable
            && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                if (entry.Entity is ITrackable)
                {
                    if (entry.State == EntityState.Added)
                    {
                        ((ITrackable)entry.Entity).CreationDate = DateTime.UtcNow;
                    }

                    ((ITrackable)entry.Entity).ModifiedDate = DateTime.UtcNow;

                }

                if (entry.Entity is IUserTrackable)
                {
                    if (entry.State == EntityState.Added)
                    {
                        ((IUserTrackable)entry.Entity).CreatedBy = currentUserID;
                    }

                    ((IUserTrackable)entry.Entity).ModifiedBy = currentUserID;
                }
            }
        }
    }
}
