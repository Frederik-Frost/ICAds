using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
namespace ICAds.Data
{
    public class AppDataContext : DbContext
    {

        public AppDataContext() { }
        public AppDataContext(DbContextOptions<AppDataContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseNpgsql(Config.Configuration.GetConnectionString("ICAdsDb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();

        }


        //public DbSet<Test> Test { get; set; }
    }
}
