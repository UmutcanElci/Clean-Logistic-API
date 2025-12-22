using Logistics.Domain.common;

namespace Logistics.Application.Interfaces;


public interface IGeocodingService
{
    Task<Coordinates> GetCoordinatesFromAddressAsync(string street, string city, string country, string postalCode);
}
