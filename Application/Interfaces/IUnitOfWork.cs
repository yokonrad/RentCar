namespace Application.Interfaces
{
    public interface IUnitOfWork
    {
        ICarRepository Cars { get; }

        ILocationRepository Locations { get; }

        IReservationRepository Reservations { get; }
    }
}
