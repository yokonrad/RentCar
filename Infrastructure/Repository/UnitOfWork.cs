using Application.Interfaces;

namespace Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(
            ICarRepository carRepository,
            ILocationRepository locationRepository,
            IReservationRepository reservationRepository)
        {
            Cars = carRepository;
            Locations = locationRepository;
            Reservations = reservationRepository;
        }

        public ICarRepository Cars { get; }

        public ILocationRepository Locations { get; }

        public IReservationRepository Reservations { get; }
    }
}