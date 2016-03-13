using MoM.Module.Interfaces;
using MoM.Module.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace MoM.Module.Managers
{
    public class DataStorageManager : IDataStorage
    {
        public static string ConnectionString { get; set; }
        public static IEnumerable<Assembly> Assemblies { get; set; }

        public ApplicationDbContext StorageContext { get; private set; }

        public DataStorageManager()
        {
            StorageContext = new ApplicationDbContext(ConnectionString, Assemblies);
            //StorageContext.Database.EnsureCreatedAsync();
        }

        public TRepository GetRepository<TRepository>() where TRepository : IDataRepository
        {
            foreach (Assembly assembly in Assemblies.Where(a => !a.FullName.Contains("Reflection")))
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (typeof(TRepository).IsAssignableFrom(type) && type.GetTypeInfo().IsClass)
                    {
                        TRepository repository = (TRepository)Activator.CreateInstance(type);
                        repository.SetStorageContext(StorageContext);
                        return repository;
                    }
                }
            }

            throw new ArgumentException("Implementation of " + typeof(TRepository) + " not found");
        }

        public void SaveChanges()
        {
            StorageContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await StorageContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            if (StorageContext != null)
                StorageContext.Dispose();
        }
    }
}
