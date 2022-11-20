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
                entity.Property(e => e.OrganizationId).HasMaxLength(36).IsFixedLength();

                entity.HasOne(e => e.Organization)
                .WithMany(e => e.Users)
                .HasForeignKey(e => e.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<OrganizationModel>(entity =>
            {
                entity.Property(e => e.Id).IsRequired().HasMaxLength(36).IsFixedLength();
                entity.Property(e => e.Name).IsRequired().HasMaxLength(120);

                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<IntegrationModel>(entity =>
            {
                entity.Property(e => e.Id).IsRequired().HasMaxLength(36).IsFixedLength();
                entity.Property(e => e.Name).HasMaxLength(120);
                entity.Property(e => e.Url).HasMaxLength(200);
                entity.Property(e => e.AccessToken).HasMaxLength(40);
                entity.Property(e => e.OrganizationId).HasMaxLength(36).IsFixedLength();

                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Organization)
                .WithMany(e => e.Integrations)
                .HasForeignKey(e => e.OrganizationId);
            });

            modelBuilder.Entity<TemplateMetadataModel>(entity =>
            {
                entity.Property(e => e.Id).IsRequired().HasMaxLength(36).IsFixedLength();
                entity.Property(e => e.Name).HasMaxLength(120);
                entity.Property(e => e.CreatedBy).HasMaxLength(36).IsFixedLength();
                entity.Property(e => e.UpdatedBy).HasMaxLength(36).IsFixedLength();
                entity.Property(e => e.IsPublic).HasDefaultValue(false);
                entity.Property(e => e.IntegrationId).HasMaxLength(36).IsFixedLength();
                entity.Property(e => e.OrganizationId).HasMaxLength(36).IsFixedLength();


                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Integration)
                .WithMany(e => e.TemplateMetadata)
                .HasForeignKey(e => e.IntegrationId);

                entity.HasOne(e => e.Template)
                .WithOne(e => e.TemplateMetadata)
                .HasForeignKey<TemplateModel>(e => e.TemplateMetadataId)
                .OnDelete(DeleteBehavior.Cascade);

            });
            


            modelBuilder.Entity<TemplateModel>(entity =>
            {
                entity.Property(e => e.Id).IsRequired().HasMaxLength(36).IsFixedLength();
                entity.Property(e => e.TemplateMetadataId).HasMaxLength(36).IsFixedLength();

                entity.HasKey(e => e.Id);
            });
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<OrganizationModel> Organizations { get; set; }
        public DbSet<IntegrationModel> Integrations { get; set; }
        public DbSet<TemplateMetadataModel> TemplatesMetadata { get; set; }
        public DbSet<TemplateModel> Templates { get; set; }

    }
}
