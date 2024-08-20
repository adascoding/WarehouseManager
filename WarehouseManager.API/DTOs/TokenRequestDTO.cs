namespace WarehouseManager.API.DTOs;

public class TokenRequestDTO
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
}
