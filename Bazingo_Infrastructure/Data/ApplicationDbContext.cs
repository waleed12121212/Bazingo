using Bazingo_Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazingo.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Shipping> Shippings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Escrow> Escrows { get; set; }
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Zone> Zones { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<ItemUnit> ItemUnits { get; set; }
        public DbSet<PriceHistory> PriceHistories { get; set; }
        public DbSet<UserActivityLog> UserActivityLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserActivityLog>().HasNoKey();

            // User Entity
            modelBuilder.Entity<User>()
                .HasMany(u => u.Products)
                .WithOne(p => p.Seller)
                .HasForeignKey(p => p.SellerID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.Buyer)
                .HasForeignKey(o => o.BuyerID)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<User>()
                .HasMany(u => u.Bids)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Reviews)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserID);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Complaints)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserID)
                .OnDelete(DeleteBehavior.Restrict);


            // Product Entity
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryID);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.BaseCurrency)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.BaseCurrencyID);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.OrderDetails)
                .WithOne(od => od.Product)
                .HasForeignKey(od => od.ProductID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Auctions)
                .WithOne(a => a.Product)
                .HasForeignKey(a => a.ProductID);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Reviews)
                .WithOne(r => r.Product)
                .HasForeignKey(r => r.ProductID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.PriceHistories)
                .WithOne(ph => ph.Product)
                .HasForeignKey(ph => ph.ProductID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.ItemUnits)
                .WithOne(iu => iu.Product)
                .HasForeignKey(iu => iu.ProductID);

            // Category Entity
            modelBuilder.Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany(pc => pc.SubCategories)
                .HasForeignKey(c => c.ParentCategoryID);

            // Order Entity
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Buyer)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.BuyerID)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Order>()
                .HasOne(o => o.Payment)
                .WithOne(p => p.Order)
                .HasForeignKey<Payment>(p => p.OrderID);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Shipping)
                .WithOne(s => s.Order)
                .HasForeignKey<Shipping>(s => s.OrderID);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Escrow)
                .WithOne(e => e.Order)
                .HasForeignKey<Escrow>(e => e.OrderID);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderDetails)
                .WithOne(od => od.Order)
                .HasForeignKey(od => od.OrderID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.Complaints)
                .WithOne(c => c.Order)
                .HasForeignKey(c => c.OrderID);

            // OrderDetail Entity
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductID);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderID);

            // Payment Entity
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Order)
                .WithOne(o => o.Payment)
                .HasForeignKey<Payment>(p => p.OrderID);

            // Shipping Entity
            modelBuilder.Entity<Shipping>()
                .HasOne(s => s.Order)
                .WithOne(o => o.Shipping)
                .HasForeignKey<Shipping>(s => s.OrderID);

            // Review Entity
            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserID);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProductID);

            // Complaint Entity
            modelBuilder.Entity<Complaint>()
                .HasOne(c => c.User)
                .WithMany(u => u.Complaints)
                .HasForeignKey(c => c.UserID)
                .OnDelete(DeleteBehavior.Restrict); // أو .OnDelete(DeleteBehavior.NoAction) أو .OnDelete(DeleteBehavior.SetNull)


            modelBuilder.Entity<Complaint>()
                .HasOne(c => c.Order)
                .WithMany(o => o.Complaints)
                .HasForeignKey(c => c.OrderID)
                .OnDelete(DeleteBehavior.Restrict);



            // Auction Entity
            modelBuilder.Entity<Auction>()
                .HasOne(a => a.Product)
                .WithMany(p => p.Auctions)
                .HasForeignKey(a => a.ProductID);


            // Bid Entity
            modelBuilder.Entity<Bid>()
                .HasOne(b => b.Auction)
                .WithMany(a => a.Bids)
                .HasForeignKey(b => b.AuctionID);

            modelBuilder.Entity<Bid>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bids)
                .HasForeignKey(b => b.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            // Zone Entity
            modelBuilder.Entity<Zone>()
                .HasOne(z => z.City)
                .WithMany(c => c.Zones)
                .HasForeignKey(z => z.CityID);

            // ShoppingCartItem Entity
            modelBuilder.Entity<ShoppingCartItem>()
                .HasOne(sci => sci.Buyer)
                .WithMany(u => u.ShoppingCartItems)
                .HasForeignKey(sci => sci.BuyerID);

            modelBuilder.Entity<ShoppingCartItem>()
                .HasOne(sci => sci.Product)
                .WithMany(p => p.ShoppingCartItems)
                .HasForeignKey(sci => sci.ProductID);

            // ItemUnit Entity
            modelBuilder.Entity<ItemUnit>()
                .HasOne(iu => iu.Product)
                .WithMany(p => p.ItemUnits)
                .HasForeignKey(iu => iu.ProductID);

            modelBuilder.Entity<ItemUnit>()
                .HasOne(iu => iu.Unit)
                .WithMany(u => u.ItemUnits)
                .HasForeignKey(iu => iu.UnitID);

            // PriceHistory Entity
            modelBuilder.Entity<PriceHistory>()
                .HasOne(ph => ph.Product)
                .WithMany(p => p.PriceHistories)
                .HasForeignKey(ph => ph.ProductID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PriceHistory>()
                .HasOne(ph => ph.Currency)
                .WithMany(c => c.PriceHistories)
                .HasForeignKey(ph => ph.CurrencyID);

        }
    }
}
