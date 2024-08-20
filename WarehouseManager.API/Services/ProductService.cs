using AutoMapper;
using WarehouseManager.API.DTOs;
using WarehouseManager.API.Models;
using WarehouseManager.API.Repositories.Interfaces;
using WarehouseManager.API.Services.Interfaces;

namespace WarehouseManager.API.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ProductDTO> GetProductByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        return _mapper.Map<ProductDTO>(product);
    }

    public async Task<PagedList<ProductDTO>> GetAllProductsAsync(int pageNumber, int pageSize)
    {
        var pagedProducts = await _productRepository.GetAllPaginatedAsync(pageNumber, pageSize);
        return _mapper.Map<PagedList<ProductDTO>>(pagedProducts);
    }

    public async Task<ProductDTO> AddProductAsync(CreateProductDTO createProductDto)
    {
        var product = _mapper.Map<Product>(createProductDto);
        var addedProduct = await _productRepository.AddAsync(product);
        await _productRepository.SaveChangesAsync();
        return _mapper.Map<ProductDTO>(addedProduct);
    }

    public async Task<ProductDTO> UpdateProductAsync(UpdateProductDTO updateProductDto)
    {
        var product = await _productRepository.GetByIdAsync(updateProductDto.Id);
        if (product == null) return null;

        _mapper.Map(updateProductDto, product);
        var updatedProduct = await _productRepository.UpdateAsync(product);
        await _productRepository.SaveChangesAsync();
        return _mapper.Map<ProductDTO>(updatedProduct);
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        return await _productRepository.DeleteAsync(id);
    }
}