using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Realtea.Domain.Entities;
using Realtea.Domain.Enums;

namespace Realtea.Infrastructure.Seeder
{
    public static class DatabaseInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var db = scope.ServiceProvider.GetRequiredService<RealTeaDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

            if (!db.Database.EnsureCreated()) return;

            if (db.Advertisements.Any()) return;


            await roleManager.CreateAsync(new IdentityRole<int> { Name = "Normal" });
            await roleManager.CreateAsync(new IdentityRole<int> { Name = "Broker" });

            var user = new User
            {
                Id = 1,
                UserName = "testuser",
                UserBalance = new UserBalance
                {
                    Balance = 10.0m,
                    UserId = 1
                },
                Advertisements = new List<Advertisement>
                {
                    new Advertisement
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
                    },
                    new Advertisement
                    {
                        Id = 4,
                        Name = "Newly-constructed house with pool is on rent",
                        Description = "Newly constructed house in Batumi with pool and veranda is available for rent. It is ideal for people that are willing to celebrate their" +
                        "birthdays or want to organize a party with their friends",
                        AdvertisementDetails = new AdvertisementDetails
                        {
                            Id = 4,
                            AdvertisementId = 4,
                            DealType = DealType.Rental,
                            Price = 1000.0m,
                            Location = Location.Batumi,
                            SquareMeter = 451.6m,
                        }
                    },
                    new Advertisement
                    {
                        Id = 5,
                        Name = "Flat on sale in Tbilisi center",
                        Description = "nice and cozy flat is on sale right in the center of capital Tbilisi, Tchavtchavadze avenue. flat is on 22nd floor with stunning views.",
                        AdvertisementDetails = new AdvertisementDetails
                        {
                            Id = 5,
                            AdvertisementId = 5,
                            DealType = DealType.Sale,
                            Price = 175000m,
                            Location = Location.Tbilisi,
                            SquareMeter = 87.1m,
                        }
                    },
                }
            };

            var result = userManager.CreateAsync(user, "Test1");
            await db.SaveChangesAsync();

            await userManager.AddToRoleAsync(user, "Normal");
        }

    }
}
