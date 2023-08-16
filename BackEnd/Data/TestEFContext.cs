using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestWebAPPEF.Models;

namespace TestWebAPPEF.Data
{
    public class TestEFContext : DbContext
    {
        public TestEFContext (DbContextOptions<TestEFContext> options)
            : base(options)
        {
        }

        public DbSet<TestWebAPPEF.Models.Movie> Movie { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().HasOne<Producer>().WithMany().HasForeignKey(v => v.ProducerId);
        }



        public DbSet<TestWebAPPEF.Models.Producer>? Producer { get; set; }

        public DbSet<TestWebAPPEF.Models.Director>? Director { get; set; }
    }
}
