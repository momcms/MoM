using Microsoft.Data.Entity;
using MoM.Module.Interfaces;
using MoM.Module.Models;

namespace MoM.Module.Base
{
    public abstract class RepositoryBase<TEntity> : IDataRepository where TEntity : class, IDataEntity
    {
        protected ApplicationDbContext Db;
        protected DbSet<TEntity> DbSet;

        public void SetStorageContext(IApplicationDbContext db)
        {
            Db = db as ApplicationDbContext;
            DbSet = Db.Set<TEntity>();
        }
    }
}
