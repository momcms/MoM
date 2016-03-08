using Microsoft.Data.Entity;
using MoM.Module.Interfaces;
using MoM.Module.Managers;

namespace MoM.Module.Base
{
    public abstract class RepositoryBase<TEntity> : IDataRepository where TEntity : class, IDataEntity
    {
        protected DataStorageContextManager DatabaseContext;
        protected DbSet<TEntity> DatabaseSet;

        public void SetStorageContext(IDataStorageContext dbContext)
        {
            DatabaseContext = dbContext as DataStorageContextManager;
            DatabaseSet = DatabaseContext.Set<TEntity>();
        }
    }
}
