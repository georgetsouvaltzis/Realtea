using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Realtea.Core.Entities;
using Realtea.Core.Enums;
using Realtea.Core.Interfaces.Repositories;
using Realtea.Infrastructure.Identity;

namespace Realtea.Infrastructure.Seeder
{
    public static class DatabaseInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var db = scope.ServiceProvider.GetRequiredService<RealTeaDbContext>();
            var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            if (!db.Database.EnsureCreated()) return;

            if (db.Advertisements.Any()) return;


            await roleManager.CreateAsync(new ApplicationRole { Name = "Normal" });
            await roleManager.CreateAsync(new ApplicationRole { Name = "Broker" });

            //await userManager.CreateAsync(new ApplicationUser
            //{
            //    Id = 1,
            //    UserName = "testuser"
            //});

            await db.SaveChangesAsync();

            var result = await userRepository.CreateAsync(new User
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
                        UserId = 1,
                        Name = "Newly constructed appartment in Vake",
                        Description = "117 sq.m2 appartment is on sale in Vake, newly-constructed building at Chavchavadze avenue.",
                        AdvertisementType = AdvertisementType.Free,
                        DealType = DealType.Sale,
                        Price = 85000,
                        Location = Location.Tbilisi,
                        SquareMeter = 117.4m,
                        IsActive = true,
                    },
                    new Advertisement
                    {
                        Id = 2,
                        UserId = 1,
                        Name = "Appartment room for rent in Old-Town",
                        Description = "An appartment has 3 bedrooms and 1 common Bathroom. It is ideal and most-suitable for students. It has a metro and Mall nearby.",
                        AdvertisementType = AdvertisementType.Free,
                        DealType = DealType.Rental,
                        Price = 400m,
                        Location = Location.Tbilisi,
                        SquareMeter = 150m,
                        IsActive = true,
                    },
                    new Advertisement
                    {
                        Id = 3,
                        UserId = 1,
                        Name = "Appartment Available for Mortgage",
                        Description = "Small nice flat located in New Batumi in front of boulevard is available for mortgage. It has bathroom, 2 bedrooms, 1 studio and small veranda.",
                        DealType = DealType.Mortgage,
                        Price = 10000m,
                        Location = Location.Batumi,
                        SquareMeter = 80.7m,
                    },
                    new Advertisement
                    {
                        Id = 4,
                        UserId = 1,
                        Name = "Newly-constructed house with pool is on rent",
                        Description = "Newly constructed house in Batumi with pool and veranda is available for rent. It is ideal for people that are willing to celebrate their" +
                        "birthdays or want to organize a party with their friends",
                        DealType = DealType.Rental,
                        Price = 1000.0m,
                        Location = Location.Batumi,
                        SquareMeter = 451.6m,
                    },
                    new Advertisement
                    {
                        Id = 5,
                        UserId = 1,
                        Name = "Flat on sale in Tbilisi center",
                        Description = "nice and cozy flat is on sale right in the center of capital Tbilisi, Tchavtchavadze avenue. flat is on 22nd floor with stunning views.",
                        DealType = DealType.Sale,
                        Price = 175000m,
                        Location = Location.Tbilisi,
                        SquareMeter = 87.1m,
                    },
                }
            }, "Test1");

            await db.SaveChangesAsync();

            //await userManager.AddToRoleAsync(user, "Normal");
        }

    }
}
