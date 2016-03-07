using MoM.Module.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MoM.Module.Managers
{
    public class DataStorageManager : IDataStorage
    {
        public static string ConnectionString { get; set; }
        public static IEnumerable<Assembly> Assemblies { get; set; }

        public DataStorageContextManager StorageContext { get; private set; }

        public DataStorageManager()
        {
            StorageContext = new DataStorageContextManager(ConnectionString, Assemblies);
            StorageContext.Database.EnsureCreatedAsync();
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

        public void Save()
        {
            StorageContext.SaveChanges();
        }
    }
}
