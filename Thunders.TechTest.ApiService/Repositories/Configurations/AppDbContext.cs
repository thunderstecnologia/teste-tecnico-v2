﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Thunders.TechTest.ApiService.Models;

namespace Thunders.TechTest.ApiService.Repositories.Configurations
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TollRecord> TollRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aplicando configurações de entidades separadas
            modelBuilder.ApplyConfiguration(new TollRecordConfiguration());
        }
    }
}
