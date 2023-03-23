using Microsoft.Extensions.DependencyInjection;
using Realtea.Domain.Entities;
using Realtea.Domain.Enums;

namespace Realtea.Infrastructure.Seeder
{
    public static class DatabaseInitializer
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var db = scope.ServiceProvider.GetRequiredService<RealTeaDbContext>();

            if (!db.Database.EnsureCreated()) return;

            if (db.Advertisements.Any()) return;

            db.Advertisements.AddRange(new Advertisement
            {
                Id = 1,
                Name = "Newly constructed appartment in Vake",
                Description = "117 sq.m2 appartment is on sale in Vake, newly-constructed building at Chavchavadze avenue.",
                AdvertisementType = AdvertisementType.Free,
                AdvertisementDetails = new AdvertisementDetails
                {
                    Id = 1,
                    AdvertisementId = 1,
                    DealType = DealType.Sale,
                    Price = 85000,
                    Location = Location.Tbilisi,
                    SquareMeter = 117.4m,
                },
            },
            new Advertisement
            {
                Id = 2,
                Name = "Appartment room for rent in Old-Town",
                Description = "An appartment has 3 bedrooms and 1 common Bathroom. It is ideal and most-suitable for students. It has a metro and Mall nearby.",
                AdvertisementType = AdvertisementType.Free,
                AdvertisementDetails = new AdvertisementDetails
                {
                    Id = 2,
                    AdvertisementId = 2,
                    DealType = DealType.Rental,
                    Price = 400m,
                    Location = Location.Tbilisi,
                    SquareMeter = 150m,
                }
            },
            new Advertisement
            {
                Id = 3,
                Name = "Appartment Available for Mortgage",
                Description = "Small nice flat located in New Batumi in front of boulevard is available for mortgage. It has bathroom, 2 bedrooms, 1 studio and small veranda.",
                AdvertisementDetails = new AdvertisementDetails
                {
                    Id = 3,
                    AdvertisementId = 3,
                    DealType = DealType.Mortgage,
                    Price = 10000m,
                    Location = Location.Batumi,
                    SquareMeter = 80.7m,
                }
            });

            db.SaveChanges();
        }
    }
}
