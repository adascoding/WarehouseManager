using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseManager.API.DTOs;
using WarehouseManager.API.Services.Interfaces;

namespace WarehouseManager.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
    private readonly IProductService _productService = productService;

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductByIdAsync(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProductsAsync([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var pagedProducts = await _productService.GetAllProductsAsync(pageNumber, pageSize);
        return Ok(pagedProducts);
    }

    [HttpPost]
    public async Task<IActionResult> AddProductAsync([FromBody] CreateProductDTO createProductDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var addedProduct = await _productService.AddProductAsync(createProductDto);
        return CreatedAtAction(nameof(GetProductByIdAsync), new { id = addedProduct.Id }, addedProduct);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProductAsync([FromBody] UpdateProductDTO updateProductDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updatedProduct = await _productService.UpdateProductAsync(updateProductDto);
        if (updatedProduct == null)
            return NotFound();

        return Ok(updatedProduct);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductAsync(int id)
    {
        var result = await _productService.DeleteProductAsync(id);
        if (!result)
            return NotFound();

        return NoContent();
    }
}