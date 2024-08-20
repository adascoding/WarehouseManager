using WarehouseManager.API.DTOs;
using WarehouseManager.API.Models;

namespace WarehouseManager.API.Repositories.Interfaces;

public interface IWarehouseRepository
{
    Task<Warehouse> GetByIdAsync(int id, bool includeProducts = false);
    Task<PagedList<Warehouse>> GetAllPaginatedAsync(int pageNumber, int pageSize, bool includeProducts = false);
    Task<IEnumerable<Product>> GetProductsByWarehouseIdAsync(int warehouseId);
    Task<Warehouse> AddAsync(Warehouse warehouse);
    Task<Warehouse> UpdateAsync(Warehouse warehouse);
    Task<bool> DeleteAsync(int id);
    Task<bool> SaveChangesAsync();
}