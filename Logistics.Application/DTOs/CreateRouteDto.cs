namespace Logistics.Application.DTOs;

using Logistics.Domain.common;
// Vehciel Dto problem
public class CreateRouteDto
{
    public required Guid OrderId { get; set; }
}
