using Application.Interfaces;
using Core.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Infrastructure.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private readonly IConfiguration _configuration;

        public LocationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IReadOnlyList<Location>> GetAllAsync()
        {
            var sql = @"SELECT * FROM Locations";

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            var result = await connection.QueryAsync<Location>(sql);

            return result.ToList();
        }

        public async Task<Location?> GetByIdAsync(int id)
        {
            var sql = @"SELECT * FROM Locations
                        WHERE Id = @Id";

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            var result = await connection.QueryAsync<Location>(sql, new { Id = id });

            return result.SingleOrDefault();
        }

        public async Task<int> AddAsync(Location entity)
        {
            var sql = @"INSERT INTO Locations
                        (Name)
                        VALUES
                        (@Name)";

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            var result = await connection.ExecuteAsync(sql, entity);

            return result;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = @"DELETE FROM Locations
                        WHERE Id = @Id";

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            var result = await connection.ExecuteAsync(sql, new { Id = id });

            return result;
        }

        public async Task<int> UpdateAsync(Location entity)
        {
            var sql = @"UPDATE Locations SET
                        Name = @Name
                        WHERE Id = @Id";

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            var result = await connection.ExecuteAsync(sql, entity);

            return result;
        }
    }
}