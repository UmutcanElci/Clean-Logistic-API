namespace Logistics.Application.DTOs;

public class LocationDto
{
    public required string StreetAddress { get; set; }
    public required string City { get; set; }
    public required int PostalCode { get; set; }
    public required string Country { get; set; }
}
