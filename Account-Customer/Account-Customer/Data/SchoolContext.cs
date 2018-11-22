using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountCustomer.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountCustomer.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
        }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profile>().ToTable("Profile");
            modelBuilder.Entity<Address>().ToTable("Address");
        }
    }
}

