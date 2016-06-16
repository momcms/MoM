using Microsoft.EntityFrameworkCore;

namespace MoM.Module.Config
{
    public class ConfigurationContext : DbContext
    {
        private string ConnectionString { get; set; }
        public ConfigurationContext(DbContextOptions<ConfigurationContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ConnectionString = !string.IsNullOrEmpty(ConnectionString) ? ConnectionString : "Server=.;Database=MoM;Trusted_Connection=True;MultipleActiveResultSets=true";
            base.OnConfiguring(optionsBuilder);
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(ConnectionString, b => b.MigrationsAssembly("MoM.Web"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Configuration>();
        }

        public DbSet<Configuration> Configurations { get; set; }
    }
}
