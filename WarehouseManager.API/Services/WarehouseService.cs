using AutoMapper;
using WarehouseManager.API.DTOs;
using WarehouseManager.API.Models;
using WarehouseManager.API.Repositories.Interfaces;
using WarehouseManager.API.Services.Interfaces;

namespace WarehouseManager.API.Services;

public class WarehouseService : IWarehouseService
{
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IMapper _mapper;

    public WarehouseService(IWarehouseRepository warehouseRepository, IMapper mapper)
    {
        _warehouseRepository = warehouseRepository;
        _mapper = mapper;
    }

    public async Task<WarehouseDTO> GetWarehouseByIdAsync(int id, bool includeProducts = false)
    {
        var warehouse = await _warehouseRepository.GetByIdAsync(id, includeProducts);
        return _mapper.Map<WarehouseDTO>(warehouse);
    }

    public async Task<PagedList<WarehouseDTO>> GetAllWarehousesAsync(int pageNumber, int pageSize, bool includeProducts = false)
    {
        var pagedWarehouses = await _warehouseRepository.GetAllPaginatedAsync(pageNumber, pageSize, includeProducts);
        return _mapper.Map<PagedList<WarehouseDTO>>(pagedWarehouses);
    }

    public async Task<IEnumerable<ProductDTO>> GetProductsByWarehouseIdAsync(int warehouseId)
    {
        var products = await _warehouseRepository.GetProductsByWarehouseIdAsync(warehouseId);
        return _mapper.Map<IEnumerable<ProductDTO>>(products);
    }

    public async Task<WarehouseDTO> AddWarehouseAsync(CreateWarehouseDTO createWarehouseDto)
    {
        var warehouse = _mapper.Map<Warehouse>(createWarehouseDto);
        var addedWarehouse = await _warehouseRepository.AddAsync(warehouse);
        await _warehouseRepository.SaveChangesAsync();
        return _mapper.Map<WarehouseDTO>(addedWarehouse);
    }

    public async Task<WarehouseDTO> UpdateWarehouseAsync(UpdateWarehouseDTO updateWarehouseDto)
    {
        var warehouse = await _warehouseRepository.GetByIdAsync(updateWarehouseDto.Id);
        if (warehouse == null) return null;

        _mapper.Map(updateWarehouseDto, warehouse);
        var updatedWarehouse = await _warehouseRepository.UpdateAsync(warehouse);
        await _warehouseRepository.SaveChangesAsync();
        return _mapper.Map<WarehouseDTO>(updatedWarehouse);
    }

    public async Task<bool> DeleteWarehouseAsync(int id)
    {
        return await _warehouseRepository.DeleteAsync(id);
    }
}