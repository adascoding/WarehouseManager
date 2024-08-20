using Microsoft.EntityFrameworkCore;
using WarehouseManager.API.Models;

namespace WarehouseManager.API.Data;

public class WarehouseContext(DbContextOptions<WarehouseContext> options) : DbContext(options)
{
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public static async Task Seed(WarehouseContext context)
    {
        if (context.Warehouses.Any() || context.Products.Any())
        {
            return;
        }

        var warehouses = new[]
        {
                new Warehouse { Name = "Central Warehouse", Address = "123 Main St" },
                new Warehouse { Name = "North Warehouse", Address = "456 Elm St" },
                new Warehouse { Name = "South Warehouse", Address = "789 Pine St" }
            };

        await context.Warehouses.AddRangeAsync(warehouses);
        await context.SaveChangesAsync();

        var products = new[]
        {
                new Product { Name = "Widget", Description = "A useful widget", Price = 19.99m, WarehouseId = 1, MinQuanity = 10, CurrentQuantity = 50 },
                new Product { Name = "Gadget", Description = "An amazing gadget", Price = 29.99m, WarehouseId = 1, MinQuanity = 15, CurrentQuantity = 30 },
                new Product { Name = "Thingamajig", Description = "A handy thingamajig", Price = 39.99m, WarehouseId = 2, MinQuanity = 5, CurrentQuantity = 20 },
                new Product { Name = "Doodad", Description = "A useful doodad", Price = 49.99m, WarehouseId = 2, MinQuanity = 8, CurrentQuantity = 40 },
                new Product { Name = "Doohickey", Description = "An essential doohickey", Price = 59.99m, WarehouseId = 3, MinQuanity = 20, CurrentQuantity = 15 },
                new Product { Name = "Contraption", Description = "A complex contraption", Price = 69.99m, WarehouseId = 3, MinQuanity = 25, CurrentQuantity = 10 }
            };

        await context.Products.AddRangeAsync(products);
        await context.SaveChangesAsync();
    }
}
