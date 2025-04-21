using Microsoft.Data.SqlClient;
using StockMarket.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using StockMarket.StockMarket.Models.StockMarket.Interfaces;

namespace StockMarket.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly string _connectionString;

        public CompanyService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Company>> GetAllAsync()
        {
            var companies = new List<Company>();
            using SqlConnection conn = new(_connectionString);
            string query = "SELECT * FROM Companies";
            using SqlCommand cmd = new(query, conn);

            await conn.OpenAsync();
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                companies.Add(new Company
                {
                    Id = (int)reader["Id"],
                    CompanyName = reader["CompanyName"].ToString(),
                    TradingRate = (int)reader["TradingRate"],
                    TradingVolume = (int)reader["TradingVolume"],
                    TradingCost = (int)reader["TradingCost"],
                    TradingCurrency = reader["TradingCurrency"].ToString()
                });
            }
            return companies;
        }

        public async Task<Company> GetByIdAsync(int id)
        {
            using SqlConnection conn = new(_connectionString);
            string query = "SELECT * FROM Companies WHERE Id = @Id";
            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@Id", id);

            await conn.OpenAsync();
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new Company
                {
                    Id = (int)reader["Id"],
                    CompanyName = reader["CompanyName"].ToString(),
                    TradingRate = (int)reader["TradingRate"],
                    TradingVolume = (int)reader["TradingVolume"],
                    TradingCost = (int)reader["TradingCost"],
                    TradingCurrency = reader["TradingCurrency"].ToString()
                };
            }
            return null;
        }

        public async Task AddAsync(Company company)
        {
            using SqlConnection conn = new(_connectionString);
            string query = @"INSERT INTO Companies 
                             (CompanyName, TradingRate, TradingVolume, TradingCost, TradingCurrency) 
                             VALUES (@CompanyName, @TradingRate, @TradingVolume, @TradingCost, @TradingCurrency)";
            using SqlCommand cmd = new(query, conn);

            cmd.Parameters.AddWithValue("@CompanyName", company.CompanyName);
            cmd.Parameters.AddWithValue("@TradingRate", company.TradingRate);
            cmd.Parameters.AddWithValue("@TradingVolume", company.TradingVolume);
            cmd.Parameters.AddWithValue("@TradingCost", company.TradingCost);
            cmd.Parameters.AddWithValue("@TradingCurrency", company.TradingCurrency);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync(Company company)
        {
            using SqlConnection conn = new(_connectionString);
            string query = @"UPDATE Companies SET 
                             CompanyName = @CompanyName,
                             TradingRate = @TradingRate,
                             TradingVolume = @TradingVolume,
                             TradingCost = @TradingCost,
                             TradingCurrency = @TradingCurrency
                             WHERE Id = @Id";
            using SqlCommand cmd = new(query, conn);

            cmd.Parameters.AddWithValue("@Id", company.Id);
            cmd.Parameters.AddWithValue("@CompanyName", company.CompanyName);
            cmd.Parameters.AddWithValue("@TradingRate", company.TradingRate);
            cmd.Parameters.AddWithValue("@TradingVolume", company.TradingVolume);
            cmd.Parameters.AddWithValue("@TradingCost", company.TradingCost);
            cmd.Parameters.AddWithValue("@TradingCurrency", company.TradingCurrency);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using SqlConnection conn = new(_connectionString);
            string query = "DELETE FROM Companies WHERE Id = @Id";
            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@Id", id);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }
    }
}
