namespace NaBarber.Services;

/// <summary>
/// Simple location model - property names match JS geolocation API
/// </summary>
public record Location
{
    public double Latitude { get; init; } = 59.3293;  // Stockholm default
    public double Longitude { get; init; } = 18.0686;
}
