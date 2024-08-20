namespace WarehouseManager.API.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int MinQuanity { get; set; }
    public int CurrentQuantity { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public DateTime? DateOfManufacture {  get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public int WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; }
}
