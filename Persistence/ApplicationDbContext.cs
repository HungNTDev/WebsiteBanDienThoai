using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Inventory>()
                .HasOne(i => i.ProductItem)
                .WithOne(pi => pi.Inventory)
                .HasForeignKey<ProductItem>(pi => pi.InventoryId);

            builder.Entity<ProductConfig>()
                .HasOne(pc => pc.ProductItem)
                .WithMany(p => p.ProductConfigs)
                .HasForeignKey(pc => pc.ProductItemId)
                .OnDelete(DeleteBehavior.Restrict); // Thay vì Cascade

            builder.Entity<ProductConfig>()
                .HasOne(pc => pc.VariationOption)
                .WithMany(v => v.ProductConfigs)
                .HasForeignKey(pc => pc.VariationOptionId)
                .OnDelete(DeleteBehavior.Restrict); // Thay vì Cascade

            builder.Entity<Payment>()
                .HasOne(p => p.UserPayment)
                .WithMany(up => up.Payments)
                .HasForeignKey(p => p.UserPaymentId)
                .OnDelete(DeleteBehavior.Restrict); // Hoặc DeleteBehavior.SetNull

            builder.Entity<Review>()
                .HasOne(r => r.OrderItem)
                .WithMany(oi => oi.Reviews)
                .HasForeignKey(r => r.OrderItemId)
                .OnDelete(DeleteBehavior.Restrict); // 🔥 Thay vì Cascade

            builder.Entity<CategoryBrand>()
                  .HasKey(ua => new { ua.BrandId, ua.CategoryId });

            builder.Entity<ProductConfig>()
                .HasKey(pc => new { pc.ProductItemId, pc.VariationOptionId });

            builder.Entity<PromotionCategory>()
                  .HasKey(pc => new { pc.PromotionId, pc.CategoryId });

            builder.Entity<UserAddress>()
                        .HasKey(ua => new { ua.UserId, ua.AddressId });

            builder.Entity<IdentityUserLogin<Guid>>().ToTable("AspNetUserLogins")
                        .HasKey(l => new { l.LoginProvider, l.ProviderKey });

            builder.Entity<IdentityUserRole<Guid>>().ToTable("AspNetUserRoles")
                        .HasKey(r => new { r.UserId, r.RoleId });

            builder.Entity<IdentityUserToken<Guid>>().ToTable("AspNetUserTokens")
                        .HasKey(t => new { t.UserId, t.LoginProvider, t.Name });

            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("AspNetRoleClaims");

            builder.Entity<IdentityUserClaim<Guid>>().ToTable("AspNetUserClaims");

            builder.Entity<ApplicationRole>().ToTable("AspNetRoles");

            builder.Entity<ApplicationUser>().ToTable("AspNetUsers");
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Combo> Comboes { get; set; }
        public DbSet<ComboProduct> ComboProducts { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductItem> ProductItems { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<PromotionCategory> PromotionCategories { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ShippingMethod> ShippingMethods { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<UserPayment> UserPayments { get; set; }
        public DbSet<Variation> Variations { get; set; }
        public DbSet<VariationOption> VariationOptions { get; set; }
        public DbSet<ProductConfig> ProductConfigs { get; set; }

    }
}
