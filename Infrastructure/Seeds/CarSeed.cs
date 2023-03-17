using Core.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Seeds
{
    public static class CarSeed
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new DataContext(serviceProvider.GetRequiredService<DbContextOptions<DataContext>>());

            if (context.Cars.Any())
            {
                return;
            }

            var list = new List<Car>
            {
                new Car {
                    Name = "Tesla Model S",
                    Description = "Model S is built from the ground up as an electric vehicle, with a high-strength architecture and floor-mounted battery pack for incredible occupant protection and low rollover risk.",
                    Price = 90,
                },

                new Car {
                    Name = "Tesla Model 3",
                    Description = "Model 3 is designed for electric-powered performance, with quick acceleration, long range and fast charging.",
                    Price = 43,
                },

                new Car {
                    Name = "Tesla Model X",
                    Description = "Model X is built for utility and performance, with standard AWD, best in class storage and the highest towing capacity of any electric SUV.",
                    Price = 100,
                },

                new Car {
                    Name = "Tesla Model Y",
                    Description = "Model Y is a fully electric, mid-size SUV with unparalleled protection and versatile cargo space.",
                    Price = 55,
                }
            };

            foreach (var item in list) context.Add(item);

            context.SaveChanges();
        }
    }
}
