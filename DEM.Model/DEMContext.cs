﻿using DEM.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEM.EF
{
    public class DEMContext : DbContext
    {
        public DEMContext(DbContextOptions<DEMContext> options) : base(options) { }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Payer> Payers { get; set; }
        public DbSet<Intended> Intendeds { get; set; }
        public DbSet<IntendedDetail> IntendedDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DEMEntityConfiguration());
        }
    }
}
