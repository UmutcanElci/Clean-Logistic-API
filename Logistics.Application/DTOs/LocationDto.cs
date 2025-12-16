namespace Logistics.Application.DTOs;

using System.ComponentModel.DataAnnotations;
public class LocationDto
{
    [Required] public required string StreetAddress { get; set; }
    [Required] public required string City { get; set; }
    [Required] public required int PostalCode { get; set; }
    [Required] public required string Country { get; set; }
}
