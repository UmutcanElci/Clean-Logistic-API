namespace Logistics.Domain.common;

public class Coordinates
{
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }

    public Coordinates(double latitude, double longitude)
    {
        // Need to learn the logic of latitude and longitude
        Latitude = latitude;
        Longitude = longitude;
    }
}
