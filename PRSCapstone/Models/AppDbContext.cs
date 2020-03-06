﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PRSCapstone.Models {
    public class AppDbContext : DbContext {

        public AppDbContext(DbContextOptions<AppDbContext> context) : base(context) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Vendor> Vendors { get; set; }

        protected override void OnModelCreating(ModelBuilder model) {
            model.Entity<User>(u => {
                u.HasIndex(x => x.Username).IsUnique();
            });
            model.Entity<Vendor>(v => {
                v.HasIndex(x => x.Code).IsUnique();
            });
            model.Entity<Product>(p => {
                p.HasIndex(x => x.PartNbr).IsUnique();
            });
        }
    }

}