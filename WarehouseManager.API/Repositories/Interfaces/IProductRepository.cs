using WarehouseManager.API.DTOs;
using WarehouseManager.API.Models;

namespace WarehouseManager.API.Repositories.Interfaces;

public interface IProductRepository
{
    Task<Product> GetByIdAsync(int id);
    Task<PagedList<Product>> GetAllPaginatedAsync(int pageNumber, int pageSize);
    Task<Product> AddAsync(Product product);
    Task<Product> UpdateAsync(Product product);
    Task<bool> DeleteAsync(int id);
    Task<bool> SaveChangesAsync();
}