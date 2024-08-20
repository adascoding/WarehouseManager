using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseManager.API.DTOs;
using WarehouseManager.API.Services.Interfaces;

namespace WarehouseManager.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class WarehousesController(IWarehouseService warehouseService) : ControllerBase
{
    private readonly IWarehouseService _warehouseService = warehouseService;

    [HttpGet("{id}")]
    public async Task<IActionResult> GetWarehouseByIdAsync(int id, [FromQuery] bool includeProducts = false)
    {
        var warehouse = await _warehouseService.GetWarehouseByIdAsync(id, includeProducts);
        if (warehouse == null)
            return NotFound();

        return Ok(warehouse);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllWarehousesAsync([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] bool includeProducts = false)
    {
        var warehouses = await _warehouseService.GetAllWarehousesAsync(pageNumber, pageSize, includeProducts);
        return Ok(warehouses);
    }

    [HttpGet("{warehouseId}/products")]
    public async Task<IActionResult> GetProductsByWarehouseIdAsync(int warehouseId)
    {
        var products = await _warehouseService.GetProductsByWarehouseIdAsync(warehouseId);
        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> AddWarehouseAsync([FromBody] CreateWarehouseDTO createWarehouseDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var addedWarehouse = await _warehouseService.AddWarehouseAsync(createWarehouseDto);
        return CreatedAtAction(nameof(GetWarehouseByIdAsync), new { id = addedWarehouse.Id }, addedWarehouse);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateWarehouseAsync([FromBody] UpdateWarehouseDTO updateWarehouseDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updatedWarehouse = await _warehouseService.UpdateWarehouseAsync(updateWarehouseDto);
        if (updatedWarehouse == null)
            return NotFound();

        return Ok(updatedWarehouse);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWarehouseAsync(int id)
    {
        var result = await _warehouseService.DeleteWarehouseAsync(id);
        if (!result)
            return NotFound();

        return NoContent();
    }
}