namespace Logistics.Application.Services;

using Logistics.Application.Interfaces;
using Logistics.Domain.common;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

public class NominatimGeocodingService : IGeocodingService
{
    private readonly HttpClient _httpClient;

    public NominatimGeocodingService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "LogisticsApp/1.0");
    }

    public async Task<Coordinates> GetCoordinatesFromAddressAsync(string street, string city, string country, string postalCode)
    {
        var url = $"https://nominatim.openstreetmap.org/search?format=json&street={street}&city={city}&country={country}&postalcode={postalCode}";

        try
        {
            var response = await _httpClient.GetFromJsonAsync<List<NominatimResult>>(url);

            if (response != null && response.Any())
            {
                var firstResult = response.First();
                return new Coordinates(double.Parse(firstResult.Lat), double.Parse(firstResult.Lon));
            }
        }
        catch
        {
        }

        return new Coordinates(0.0, 0.0);
    }

    Task<Coordinates> IGeocodingService.GetCoordinatesFromAddressAsync(string street, string city, string country, string postalCode)
    {
        throw new NotImplementedException();
    }

    private class NominatimResult
    {
        [JsonPropertyName("lat")]
        public string Lat { get; set; } = string.Empty;

        [JsonPropertyName("lon")]
        public string Lon { get; set; } = string.Empty;
    }
}
