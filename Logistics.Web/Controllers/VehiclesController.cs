using Logistics.Application.DTOs;
using Logistics.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VehiclesController : ControllerBase
{
    private readonly VehicleService _service;

    public VehiclesController(VehicleService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateVehicle([FromBody] CreateVehicleDto vehicleDto)
    {
        var createVehicle = await _service.CreateVehicleAsync(vehicleDto);

        return CreatedAtAction(nameof(GetVehicleById), new { id = createVehicle.Id }, createVehicle);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetVehicleById(Guid id)
    {
        var vehicle = await _service.GetVehicleByIdAsync(id);

        if (vehicle == null)
        {
            return NotFound();
        }

        return Ok(vehicle);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllVehicles()
    {
        var vehicles = await _service.GetAllVehiclesAsync();

        return Ok(vehicles);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateVehicle(Guid id, [FromBody] UpdateVehicleDto vehicleDto)
    {
        if (id != vehicleDto.Id)
        {
            return BadRequest();
        }
        try
        {
            await _service.UpdateVehicleAsync(vehicleDto);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteVehicle(Guid id)
    {
        await _service.DeleteVehicleAsync(id);

        return NoContent();
    }
}
