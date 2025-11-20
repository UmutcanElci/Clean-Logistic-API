public class Location
{
    public required string StreetAddress { get; set; }
    public required string City { get; set; }
    public required int PostalCode { get; set; }
    public required string Country { get; set; }
    public required Coordinates GpsCoordinates { get; set; }
}
