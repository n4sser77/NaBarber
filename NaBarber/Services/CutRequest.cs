namespace NaBarber.Services;

/// <summary>
/// A cut request from a customer
/// </summary>
public record CutRequest
{
    public string Id { get; init; } = Guid.NewGuid().ToString()[..8].ToUpper();
    public Location RequesterLocation { get; init; } = new();
    public Location BarberLocation { get; set; } = new();
    public DateTime RequestedAt { get; init; } = DateTime.Now;
    public RequestState State { get; set; } = RequestState.Pending;
}
