using Microsoft.EntityFrameworkCore;

namespace MoM.Module.Config
{
    public class ConfigurationContext : DbContext
    {
        public ConfigurationContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Configuration>();
        }

        public DbSet<Configuration> Values { get; set; }
    }
}
