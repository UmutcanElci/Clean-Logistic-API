using Logistics.Application.DTOs;
using Logistics.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HubsController : ControllerBase
{

    private readonly HubService _service;

    public HubsController(HubService service)
    {
        _service = service;
    }

    [HttpPost("generate-route")]
    public async Task<IActionResult> GenerateRoute([FromBody] CreateRouteDto routeDto)
    {
        try
        {
            var route = await _service.GenerateRouteForOrderAsync(routeDto.OrderId);

            return Ok(route);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }

    }


}
