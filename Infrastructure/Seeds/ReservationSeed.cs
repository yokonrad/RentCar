using Core.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Seeds
{
    public static class ReservationSeed
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new DataContext(serviceProvider.GetRequiredService<DbContextOptions<DataContext>>());

            if (context.Reservations.Any())
            {
                return;
            }

            var list = new List<Reservation>
            {
                new Reservation {
                    Email = "example1@example.com",
                    CarId = 1,
                    LocationFromId = 1,
                    LocationToId = 2,
                    DateFrom = new DateTime(2022, 12, 01),
                    DateTo = new DateTime(2022, 12, 15),
                },

                new Reservation
                {
                    Email = "example2@example.com",
                    CarId = 2,
                    LocationFromId = 2,
                    LocationToId = 3,
                    DateFrom = new DateTime(2023, 01, 15),
                    DateTo = new DateTime(2023, 01, 22),
                },

                new Reservation
                {
                    Email = "example3@example.com",
                    CarId = 3,
                    LocationFromId = 3,
                    LocationToId = 4,
                    DateFrom = new DateTime(2023, 02, 10),
                    DateTo = new DateTime(2023, 02, 15),
                }
            };

            foreach (var item in list) context.Add(item);

            context.SaveChanges();
        }
    }
}
