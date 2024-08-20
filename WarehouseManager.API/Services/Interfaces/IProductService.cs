using WarehouseManager.API.DTOs;

namespace WarehouseManager.API.Services.Interfaces;

public interface IProductService
{
    Task<ProductDTO> GetProductByIdAsync(int id);
    Task<PagedList<ProductDTO>> GetAllProductsAsync(int pageNumber, int pageSize);
    Task<ProductDTO> AddProductAsync(CreateProductDTO createProductDto);
    Task<ProductDTO> UpdateProductAsync(UpdateProductDTO updateProductDto);
    Task<bool> DeleteProductAsync(int id);
}