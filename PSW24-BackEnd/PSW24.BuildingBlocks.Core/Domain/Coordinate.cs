using PSW24.BuildingBlocks.Core.Domain;

namespace PSW24.BuildingBlocks.Core.Domain;

public class Coordinate : ValueObject
{
    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public Coordinate(decimal latitude, decimal longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public double DistanceTo(Coordinate otherCoordinate)
    {
        double earthRadius = 6371000;
        double lat1Rad = (double)ToRadians((double)Latitude);
        double lat2Rad = (double)ToRadians((double)otherCoordinate.Latitude);
        double deltaLat = lat2Rad - lat1Rad;
        double deltaLon = (double)ToRadians((double)otherCoordinate.Longitude - (double)Longitude);

        double a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
                   Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
                   Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);
        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        double distance = earthRadius * c;

        return distance;
    }

    private double ToRadians(double degrees)
    {
        return degrees * Math.PI / 180;
    }

    public bool IsCloseTo(Coordinate otherCoordinate, double distanceInMeters)
    {
        double distance = DistanceTo(otherCoordinate);
        return distance <= distanceInMeters;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Latitude;
        yield return Longitude;
    }
}
