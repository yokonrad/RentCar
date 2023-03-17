using Core.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Seeds
{
    public static class LocationSeed
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new DataContext(serviceProvider.GetRequiredService<DbContextOptions<DataContext>>());

            if (context.Locations.Any())
            {
                return;
            }

            var list = new List<Location>
            {
                new Location {
                    Name = "Palma Airport",
                },

                new Location {
                    Name = "Palma City Center",
                },

                new Location {
                    Name = "Alcudia",
                },

                new Location {
                    Name = "Manacor",
                }
            };

            foreach (var item in list) context.Add(item);

            context.SaveChanges();
        }
    }
}
