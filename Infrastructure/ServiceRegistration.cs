using Application.Interfaces;
using Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<ICarRepository, CarRepository>();
            services.AddTransient<ILocationRepository, LocationRepository>();
            services.AddTransient<IReservationRepository, ReservationRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
