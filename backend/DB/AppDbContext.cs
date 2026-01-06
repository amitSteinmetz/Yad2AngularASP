using backend.Models.Asset;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace backend.DB
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Asset> Assets { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Asset>(entity =>
            {
                // convert enum to string in DB
                entity.Property(a => a.Type).HasConversion<string>(); 
                entity.Property(a => a.Condition).HasConversion<string>();

                // flat Asset complex fields inside it's table
                entity.OwnsOne(a => a.Address);
                entity.OwnsOne(a => a.Publisher);
                entity.OwnsOne(a => a.Publisher);
            });
        }
    }
}
