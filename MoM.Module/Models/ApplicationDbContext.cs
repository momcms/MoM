﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoM.Module.Interfaces;
using MoM.Module.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MoM.Module.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        private string ConnectionString { get; set; }
        private IEnumerable<Assembly> Assemblies { get; set; }

        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(string connectionString, IEnumerable<Assembly> assemblies)
        {
            ConnectionString = connectionString;
            Assemblies = assemblies;
            //Database.EnsureCreated();
            Database.Migrate();

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ConnectionString = !string.IsNullOrEmpty(ConnectionString) ? ConnectionString : "Server=.;Database=MoM;Trusted_Connection=True;MultipleActiveResultSets=true";
            base.OnConfiguring(optionsBuilder);
            if(!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(ConnectionString, b => b.MigrationsAssembly("MoM.Web"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            const string IdentitySchema = "Identity";
            modelBuilder.Entity<ApplicationUser>().ToTable("User", IdentitySchema).Property(p => p.Id).HasColumnName("UserId");
            modelBuilder.Entity<IdentityRole>().ToTable("Role", IdentitySchema).Property(p => p.Id).HasColumnName("RoleId");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin", IdentitySchema);
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim", IdentitySchema);
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim", IdentitySchema);
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRole", IdentitySchema);
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserToken", IdentitySchema);
            modelBuilder.Entity<ClientRouteConfig>();
            modelBuilder.Entity<Module>();
            modelBuilder.Entity<Country>();
            modelBuilder.Entity<Language>();
            // Load all ModelBuilders from the modules
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

            //Database.Migrate();
        }
    }
}
