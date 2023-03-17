using Application.Interfaces;
using Core.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Infrastructure.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly IConfiguration _configuration;

        public CarRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IReadOnlyList<Car>> GetAllAsync()
        {
            var sql = @"SELECT * FROM Cars";

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            var result = await connection.QueryAsync<Car>(sql);

            return result.ToList();
        }

        public async Task<Car?> GetByIdAsync(int id)
        {
            var sql = @"SELECT * FROM Cars
                        WHERE Id = @Id";

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            var result = await connection.QueryAsync<Car>(sql, new { Id = id });

            return result.SingleOrDefault();
        }

        public async Task<int> AddAsync(Car entity)
        {
            var sql = @"INSERT INTO Cars
                        (Name, Description, Price)
                        VALUES
                        (@Name, @Description, @Price)";

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            var result = await connection.ExecuteAsync(sql, entity);

            return result;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = @"DELETE FROM Cars
                        WHERE Id = @Id";

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            var result = await connection.ExecuteAsync(sql, new { Id = id });

            return result;
        }

        public async Task<int> UpdateAsync(Car entity)
        {
            var sql = @"UPDATE Cars SET
                        Name = @Name,
                        Description = @Description,
                        Price = @Price
                        WHERE Id = @Id";

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            var result = await connection.ExecuteAsync(sql, entity);

            return result;
        }
    }
}