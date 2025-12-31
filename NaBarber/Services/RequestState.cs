namespace NaBarber.Services;

/// <summary>
/// Represents the current state of a dispatch request
/// </summary>
public enum RequestState
{
    Idle,           // No active request
    Pending,        // Request sent, waiting for barber
    Accepted,       // Barber accepted, on the way
    Completed       // Session finished
}
