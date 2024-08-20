namespace WarehouseManager.API.DTOs;

public class WarehouseDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public IEnumerable<ProductDTO> Products { get; set; }
}
public class CreateWarehouseDTO
{
    public string Name { get; set; }
    public string Address { get; set; }
}
public class UpdateWarehouseDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
}