﻿using EasyMarketing.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyMarketing.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<VegetableVendor> VegetableVendors { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        //}
    }
}

