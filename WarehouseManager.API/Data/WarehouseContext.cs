using Microsoft.EntityFrameworkCore;
using WarehouseManager.API.Models;

namespace WarehouseManager.API.Data;

public class WarehouseContext(DbContextOptions<WarehouseContext> options) : DbContext(options)
{
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<Product> Products { get; set; }
}
