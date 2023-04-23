using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Realtea.Core.Entities;
using Realtea.Core.Enums;
using Realtea.Core.Interfaces.Repositories;
using Realtea.Core.ValueObjects;
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

            await db.SaveChangesAsync();

            var newUser = User.Create(string.Empty, string.Empty, "testuser", string.Empty);
            await userRepository.CreateAsync(newUser, "Test1");
            newUser.AddAd(Advertisement
                    .Create("Newly constructed appartment in Vake",
                            "117 sq.m2 appartment is on sale in Vake, newly-constructed building at Chavchavadze avenue.",
                            AdvertisementType.Free,
                            true,
                            1,
                            DealType.Sale,
                            Money.Create(85000m),
                            Sq2.Create(117.4m),
                            Location.Tbilisi));

            newUser.AddAd(Advertisement
                    .Create("Appartment room for rent in Old-Town",
                    "An appartment has 3 bedrooms and 1 common Bathroom. It is ideal and most-suitable for students. It has a metro and Mall nearby.",
                    AdvertisementType.Free,
                    true,
                    1,
                    DealType.Rental,
                    Money.Create(400m),
                    Sq2.Create(150m),
                    Location.Kutaisi));


            newUser.AddAd(Advertisement
                    .Create("Appartment Available for Mortgage", "Small nice flat located in New Batumi in front of boulevard is available for mortgage. It has bathroom, 2 bedrooms, 1 studio and small veranda.",
                    AdvertisementType.Free,
                    false,
                    1,
                    DealType.Mortgage,
                    Money.Create(1000m),
                    Sq2.Create(451.6m),
                    Location.Batumi));

            newUser.AddAd(Advertisement
                    .Create("Newly-constructed house with pool is on rent", "Newly constructed house in Batumi with pool and veranda is available for rent. It is ideal for people that are willing to celebrate their" +
                        "birthdays or want to organize a party with their friends",
                        AdvertisementType.Free,
                        false,
                        1,
                        DealType.Rental,
                        Money.Create(1000),
                        Sq2.Create(451.6m),
                        Location.Batumi));

            newUser.AddAd(
                Advertisement
                    .Create("Flat on sale in Tbilisi center",
                    "nice and cozy flat is on sale right in the center of capital Tbilisi, Tchavtchavadze avenue. flat is on 22nd floor with stunning views.",
                    AdvertisementType.Free,
                    false,
                    1,
                    DealType.Sale,
                    Money.Create(175000),
                    Sq2.Create(87.1m),
                    Location.Tbilisi));
            
            await db.SaveChangesAsync();
        }
    }
}
