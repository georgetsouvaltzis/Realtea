using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Realtea.Core.Entities;
using Realtea.Infrastructure.Identity;

namespace Realtea.Infrastructure
{
    //public class RealTeaDbContext : IdentityDbContext<User, IdentityRole<int>, int
    public class RealTeaDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public RealTeaDbContext(DbContextOptions<RealTeaDbContext> options) : base(options)
        {

        }

        public DbSet<Advertisement> Advertisements { get; set; }

        public DbSet<AdvertisementDetails> AdvertisementsDetails { get; set; }

        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Advertisement>()
                .HasOne(x => x.AdvertisementDetails)
                .WithOne(x => x.Advertisement)
                .HasForeignKey<AdvertisementDetails>(x => x.AdvertisementId);

            modelBuilder.Entity<User>()
                .HasOne(x => x.UserBalance)
                .WithOne(x => x.User)
                .HasForeignKey<UserBalance>(x => x.UserId);

            base.OnModelCreating(modelBuilder);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries<BaseEntity>()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);

            foreach (EntityEntry entityEntry in entries)
            {
                var baseEntity = entityEntry.Entity as BaseEntity;
                var now = DateTimeOffset.Now;

                baseEntity!.UpdatedAt = now;

                if (entityEntry.State == EntityState.Added)
                {
                    baseEntity.CreatedAt = now;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}