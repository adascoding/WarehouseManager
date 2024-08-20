using WarehouseManager.API.DTOs;

namespace WarehouseManager.API.Services.Interfaces;

public interface IWarehouseService
{
    Task<WarehouseDTO> GetWarehouseByIdAsync(int id, bool includeProducts = false);
    Task<PagedList<WarehouseDTO>> GetAllWarehousesAsync(int pageNumber, int pageSize, bool includeProducts = false);
    Task<IEnumerable<ProductDTO>> GetProductsByWarehouseIdAsync(int warehouseId);
    Task<WarehouseDTO> AddWarehouseAsync(CreateWarehouseDTO createWarehouseDto);
    Task<WarehouseDTO> UpdateWarehouseAsync(UpdateWarehouseDTO updateWarehouseDto);
    Task<bool> DeleteWarehouseAsync(int id);
}