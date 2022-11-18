using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using ICAds.Data.Models;
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


            modelBuilder.Entity<UserModel>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).IsRequired().HasMaxLength(36).IsFixedLength();
                entity.Property(e => e.Email).IsRequired().HasMaxLength(120);
                entity.Property(e => e.IsAdmin).HasDefaultValue(false);
                entity.Property(e => e.Active).HasDefaultValue(true);
            });

        }

        public DbSet<UserModel> Users { get; set; }
        //public DbSet<Test> Test { get; set; }
    }
}
