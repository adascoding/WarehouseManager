using Microsoft.EntityFrameworkCore;
using WarehouseManager.API.Data;
using WarehouseManager.API.DTOs;
using WarehouseManager.API.Models;
using WarehouseManager.API.Repositories.Interfaces;

namespace WarehouseManager.API.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly WarehouseContext _context;

    public ProductRepository(WarehouseContext context)
    {
        _context = context;
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<PagedList<Product>> GetAllPaginatedAsync(int pageNumber, int pageSize)
    {
        var totalRecords = await _context.Products.CountAsync();
        var products = await _context.Products
            .OrderBy(x=>x.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedList<Product>(products, pageNumber, pageSize, totalRecords);
    }

    public async Task<Product> AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await SaveChangesAsync();
        return product;
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await SaveChangesAsync();
        return product;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return false;

        _context.Products.Remove(product);
        return await SaveChangesAsync();
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}