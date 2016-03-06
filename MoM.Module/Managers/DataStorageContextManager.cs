using Microsoft.Data.Entity;
using MoM.Module.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MoM.Module.Managers
{
    public class DataStorageContextManager : DbContext, IDataStorageContext
    {
        private string ConnectionString { get; set; }
        private IEnumerable<Assembly> Assemblies { get; set; }

        private static bool _created = false;

        //enable automigrations
        public DataStorageContextManager()
        {
            if (!_created)
            {
                _created = true;
                Database.MigrateAsync();
            }
        }

        public DataStorageContextManager(string connectionString, IEnumerable<Assembly> assemblies)
        {
            ConnectionString = connectionString;
            Assemblies = assemblies;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (Assembly assembly in DataStorageManager.Assemblies)
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (typeof(IDataModelRegistrator).IsAssignableFrom(type) && type.GetTypeInfo().IsClass)
                    {
                        IDataModelRegistrator modelRegistrar = (IDataModelRegistrator)Activator.CreateInstance(type);

                        modelRegistrar.RegisterModels(modelBuilder);
                    }
                }
            }
        }
    }
}
