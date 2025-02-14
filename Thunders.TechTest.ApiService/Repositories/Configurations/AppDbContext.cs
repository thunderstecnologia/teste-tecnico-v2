using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using Thunders.TechTest.ApiService.Models;

namespace Thunders.TechTest.ApiService.Repositories.Configurations
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TollRecord> TollRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .IsRequired();

            // Aplicando configurações de entidades separadas
            modelBuilder.ApplyConfiguration(new TollRecordConfiguration());
            modelBuilder.ApplyConfiguration(new TollRecordConfiguration());
        }
    }
}
