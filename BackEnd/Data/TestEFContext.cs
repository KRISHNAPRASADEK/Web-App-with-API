using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Data
{
    public class TestEFContext : DbContext
    {
        public TestEFContext(DbContextOptions<TestEFContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().HasOne<Producer>().WithMany().HasForeignKey(v => v.ProducerId);
        }



        public DbSet<Producer>? Producer { get; set; }

        public DbSet<Director>? Director { get; set; }
    }
}
