namespace WarehouseManager.API.DTOs;

public class ProductDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int WarehouseId { get; set; }
    public int MinQuantity { get; set; }
    public int CurrentQuantity { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public DateTime? DateOfManufacture { get; set; }
}

public class CreateProductDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int WarehouseId { get; set; }
    public int MinQuantity { get; set; }
    public int CurrentQuantity { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public DateTime? DateOfManufacture { get; set; }
}

public class UpdateProductDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int MinQuantity { get; set; }
    public int CurrentQuantity { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public DateTime? DateOfManufacture { get; set; }
}