using Logistics.Application.DTOs;
using Logistics.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.Web.Controllers;


[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly OrderService _service;

    public OrdersController(OrderService service)
    {
        _service = service;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetOrderById(Guid id)
    {
        var order = await _service.GetOrderByIdAsync(id);

        if (order == null)
        {
            return NotFound();
        }
        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto createOrder)
    {
        var createdOrder = await _service.CreateOrderAsync(createOrder);

        return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.Id }, createdOrder);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrders()
    {
        var orders = await _service.GetAllOrdersAsync();

        return Ok(orders);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] UpdateOrderDto orderDto)
    {
        if (id != orderDto.OrderId)
        {
            return BadRequest("Id Mismatch!");
        }

        try
        {
            await _service.UpdateOrderAsync(orderDto);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        await _service.DeleteOrderAsync(id);

        return NoContent();
    }
}
