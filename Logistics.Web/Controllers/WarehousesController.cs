using Logistics.Application.DTOs;
using Logistics.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WarehousesController : ControllerBase
{

    private readonly WarehouseService _service;

    public WarehousesController(WarehouseService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateWarehouse([FromBody] CreateWarehouseDto warehouseDto)
    {
        var warehouse = await _service.CreateWarehouseAsync(warehouseDto);

        return CreatedAtAction(nameof(GetWarehouseById), new { id = warehouse.Id }, warehouse);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllWarehouses()
    {
        var warehouses = await _service.GetAllWarehousesAsync();

        return Ok(warehouses);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetWarehouseById(Guid id)
    {
        var warehouse = await _service.GetWarehouseByIdAsync(id);

        if (id == null)
        {
            return NotFound();
        }

        return Ok(warehouse);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateWarehouse(Guid id, [FromBody] UpdateWarehouseDto warehouseDto)
    {
        if (id != warehouseDto.Id)
        {
            return BadRequest();
        }

        try
        {
            await _service.UpdateWarehouseAsync(warehouseDto);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteWarehouse(Guid id)
    {
        await _service.DeleteWarehouseAsync(id);

        return NoContent();
    }
}
