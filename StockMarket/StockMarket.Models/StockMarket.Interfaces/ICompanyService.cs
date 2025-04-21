using StockMarket.Models;

namespace StockMarket.StockMarket.Models.StockMarket.Interfaces;

public interface ICompanyService
{
    Task<List<Company>> GetAllAsync();
    Task<Company> GetByIdAsync(int i);
    Task AddAsync(Company company);
    Task UpdateAsync(Company company);
    Task DeleteAsync(int id);
}