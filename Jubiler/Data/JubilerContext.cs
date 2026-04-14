using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; 
using Jubiler.Models;

namespace Jubiler.Data
{
    public class JubilerContext : IdentityDbContext<ApplicationUser>
    {
        public JubilerContext(DbContextOptions<JubilerContext> options) : base(options) {}
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Material> Materials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Materials)
                .WithMany(m => m.Products);
        }
    }
}