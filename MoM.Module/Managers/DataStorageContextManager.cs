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


        //enable automigrations 
        public DataStorageContextManager()
        {
                //Database.MigrateAsync();
        }

        public DataStorageContextManager(string connectionString, IEnumerable<Assembly> assemblies)
        {
            ConnectionString = connectionString;
            Assemblies = assemblies;
            Database.EnsureCreated();
            //if (!_created)
            //{
                //Database.MigrateAsync();
            //}

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (Assembly assembly in DataStorageManager.Assemblies.Where(a => !a.FullName.Contains("Reflection")))
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
