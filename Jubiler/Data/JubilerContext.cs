using Microsoft.EntityFrameworkCore;
using Jubiler.Models;

namespace Jubiler.Data
{
    public class JubilerContext : DbContext
    {
        public JubilerContext(DbContextOptions<JubilerContext> options) : base(options){}
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Materials)
                .WithMany(m => m.Products);
        }

    }
}
