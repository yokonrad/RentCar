using Application.Interfaces;
using Core.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Infrastructure.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly IConfiguration _configuration;

        public ReservationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IReadOnlyList<Reservation>> GetAllAsync()
        {
            var sql = @"SELECT * FROM Reservations r
                        JOIN Cars c ON r.CarId = c.Id
                        JOIN Locations lf ON r.LocationFromId = lf.Id
                        JOIN Locations lt ON r.LocationToId = lt.Id";

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            var result = await connection.QueryAsync<Reservation, Car, Location, Location, Reservation>(sql, (reservation, car, locationFrom, locationTo) =>
            {
                reservation.Car = car;
                reservation.LocationFrom = locationFrom;
                reservation.LocationTo = locationTo;

                return reservation;
            }, splitOn: "Id");

            return result.ToList();
        }

        public async Task<Reservation?> GetByIdAsync(int id)
        {
            var sql = @"SELECT * FROM Reservations r
                        JOIN Cars c ON r.CarId = c.Id
                        JOIN Locations lf ON r.LocationFromId = lf.Id
                        JOIN Locations lt ON r.LocationToId = lt.Id
                        WHERE r.Id = @Id";

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            var result = await connection.QueryAsync<Reservation, Car, Location, Location, Reservation>(sql, (reservation, car, locationFrom, locationTo) =>
            {
                reservation.Car = car;
                reservation.LocationFrom = locationFrom;
                reservation.LocationTo = locationTo;

                return reservation;
            }, new { Id = id }, splitOn: "Id");

            return result.SingleOrDefault();
        }

        public async Task<int> AddAsync(Reservation entity)
        {
            var sql = @"INSERT INTO Reservations
                        (Email, CarId, LocationFromId, LocationToId, DateFrom, DateTo)
                        VALUES
                        (@Email, @CarId, @LocationFromId, @LocationToId, @DateFrom, @DateTo)";

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            var result = await connection.ExecuteAsync(sql, entity);

            return result;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = @"DELETE FROM Reservations
                        WHERE Id = @Id";

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            var result = await connection.ExecuteAsync(sql, new { Id = id });

            return result;
        }

        public async Task<int> UpdateAsync(Reservation entity)
        {
            var sql = @"UPDATE Reservations SET
                        Email = @Email,
                        CarId = @CarId,
                        LocationFromId = @LocationFromId,
                        LocationToId = @LocationToId,
                        DateFrom = @DateFrom,
                        DateTo = @DateTo
                        WHERE Id = @Id";

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            var result = await connection.ExecuteAsync(sql, entity);

            return result;
        }
    }
}