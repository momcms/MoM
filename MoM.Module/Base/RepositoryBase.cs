using Microsoft.Data.Entity;
using MoM.Module.Interfaces;
using MoM.Module.Managers;

namespace MoM.Module.Base
{
    public abstract class RepositoryBase<TEntity> : IDataRepository where TEntity : class, IDataEntity
    {
        protected DataStorageContextManager dbContext;
        protected DbSet<TEntity> dbSet;

        public void SetStorageContext(IDataStorageContext dbContext)
        {
            this.dbContext = dbContext as DataStorageContextManager;
            this.dbSet = this.dbContext.Set<TEntity>();
        }
    }
}
