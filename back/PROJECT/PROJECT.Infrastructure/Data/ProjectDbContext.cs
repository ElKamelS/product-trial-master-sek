using PROJECT.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace PROJECT.Infrastructure.Data
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options) { }

        public DbSet<Product> Products => Set<Product>();

        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.id);
                entity.Property(p => p.InventoryStatus)
                      .HasConversion<string>();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(p => p.id);

            });

            modelBuilder.Entity<User>()
                .HasMany(u => u.Cart)
                .WithMany(p => p.UsersCart)
                .UsingEntity(j => j.ToTable("CartProducts"));

            modelBuilder.Entity<User>()
                .HasMany(u => u.Wishlist)
                .WithMany(p => p.UsersWishlist)
                .UsingEntity(j => j.ToTable("WishlistProducts"));
        }
    }
}
