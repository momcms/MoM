using Microsoft.Data.Entity;
using MoM.Module.Interfaces;
using MoM.Module.Models;

namespace MoM.Module.Base
{
    public abstract class RepositoryBase<TEntity> : IDataRepository where TEntity : class, IDataEntity
    {
        protected ApplicationDbContext DatabaseContext;
        protected DbSet<TEntity> DbSet;

        public void SetStorageContext(IApplicationDbContext dbContext)
        {
            DatabaseContext = dbContext as ApplicationDbContext;
            DbSet = DatabaseContext.Set<TEntity>();
        }
    }
}
