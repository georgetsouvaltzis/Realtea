using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Realtea.Domain.Entities;

namespace Realtea.Infrastructure
{
    public class RealTeaDbContext : DbContext
    {
        public RealTeaDbContext(DbContextOptions<RealTeaDbContext> options) : base(options)
        {
            
        }

        public DbSet<Advertisement> Advertisements { get; set; }

        public DbSet<AdvertisementDetails> AdvertisementsDetails { get; set; }

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