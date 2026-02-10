using backend.Models;
using backend.Models.AssetModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace backend.DB
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public DbSet<Asset> Assets { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Asset>(entity =>
            {
                // flat Asset complex fields inside it's table
                entity.OwnsOne(a => a.Address);
                entity.OwnsOne(a => a.ContactDetails);

                // הגדרת הקשר 1-ל-רבים בין משתמש לנכסים
                entity.HasOne(a => a.Publisher)
                .WithMany(u => u.Assets)
                .HasForeignKey(a => a.PublisherId)
                .OnDelete(DeleteBehavior.Cascade); // אם משתמש נמחק, המודעות שלו נמחקות
            });
        }
    }
}
