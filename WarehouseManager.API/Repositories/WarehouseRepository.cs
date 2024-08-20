using Microsoft.EntityFrameworkCore;
using WarehouseManager.API.Data;
using WarehouseManager.API.DTOs;
using WarehouseManager.API.Models;
using WarehouseManager.API.Repositories.Interfaces;

namespace WarehouseManager.API.Repositories;

public class WarehouseRepository : IWarehouseRepository
{
    private readonly WarehouseContext _context;

    public WarehouseRepository(WarehouseContext context)
    {
        _context = context;
    }

    public async Task<Warehouse> GetByIdAsync(int id, bool includeProducts = false)
    {
        IQueryable<Warehouse> query = _context.Warehouses;

        if (includeProducts)
        {
            query = query.Include(w => w.Products);
        }

        return await query.FirstOrDefaultAsync(w => w.Id == id);
    }

    public async Task<PagedList<Warehouse>> GetAllPaginatedAsync(int pageNumber, int pageSize, bool includeProducts = false)
    {
        IQueryable<Warehouse> query = _context.Warehouses;

        if (includeProducts)
        {
            query = query.Include(w => w.Products);
        }

        var totalRecords = await query.CountAsync();
        var warehouses = await query
            .OrderBy(x => x.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedList<Warehouse>(warehouses, pageNumber, pageSize, totalRecords);
    }

    public async Task<IEnumerable<Product>> GetProductsByWarehouseIdAsync(int warehouseId)
    {
        return await _context.Products
                             .Where(p => p.WarehouseId == warehouseId)
                             .ToListAsync();
    }

    public async Task<Warehouse> AddAsync(Warehouse warehouse)
    {
        await _context.Warehouses.AddAsync(warehouse);
        await SaveChangesAsync();
        return warehouse;
    }

    public async Task<Warehouse> UpdateAsync(Warehouse warehouse)
    {
        _context.Warehouses.Update(warehouse);
        await SaveChangesAsync();
        return warehouse;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var warehouse = await _context.Warehouses.FindAsync(id);
        if (warehouse == null) return false;

        _context.Warehouses.Remove(warehouse);
        return await SaveChangesAsync();
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}