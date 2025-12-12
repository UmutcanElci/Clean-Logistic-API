using Logistics.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoutesController : ControllerBase
{
    private readonly RouteService _service;

    public RoutesController(RouteService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRoutes()
    {
        var routes = await _service.GetAllRoutesAsync();
        return Ok(routes);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetRouteById(Guid id)
    {
        var route = await _service.GetRouteByIdAsync(id);

        if (route == null)
        {
            return NotFound();
        }

        return Ok(route);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteRoute(Guid id)
    {
        await _service.DeleteRouteAsync(id);

        return NoContent();
    }
}
