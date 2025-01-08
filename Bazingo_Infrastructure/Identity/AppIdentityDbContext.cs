using Bazingo_Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazingo_Infrastructure.Identity
{
    public class AppIdentityDbContext : IdentityDbContext<User>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure Identity User properties if needed
            builder.Entity<User>(entity =>
            {
                entity.Property(u => u.FirstName).HasMaxLength(100).IsRequired();
                entity.Property(u => u.LastName).HasMaxLength(100).IsRequired();
                entity.Property(u => u.UserType).IsRequired();
                entity.Property(u => u.IsVerified).HasDefaultValue(false);
                entity.Property(u => u.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(u => u.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
            });

            // Configure additional relationships or indexes for User if necessary

            // Example: User and PreferredCurrency
            builder.Entity<User>()
                .HasOne(u => u.PreferredCurrency)
                .WithMany(c => c.Users)
                .HasForeignKey(u => u.PreferredCurrencyID);
        }
    }
}
