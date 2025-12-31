namespace NaBarber.Services;

/// <summary>
/// In-memory dispatch service - singleton that manages state between requester and barber
/// </summary>
public class DispatchService
{
    private CutRequest? _currentRequest;

    public event Action? OnStateChanged;
    public CutRequest? CurrentRequest => _currentRequest;

    public RequestState State => _currentRequest?.State ?? RequestState.Idle;
    public bool HasActiveRequest => _currentRequest != null && _currentRequest.State != RequestState.Completed;

    /// <summary>
    /// Requester creates a new cut request
    /// </summary>
    public void RequestCut(Location requesterLocation)
    {
        if (HasActiveRequest) return;

        _currentRequest = new CutRequest
        {
            RequesterLocation = requesterLocation,
            State = RequestState.Pending
        };

        NotifyStateChanged();
    }

    /// <summary>
    /// Barber accepts the current request
    /// </summary>
    public void AcceptRequest()
    {
        if (_currentRequest?.State != RequestState.Pending) return;

        _currentRequest.State = RequestState.Accepted;
        NotifyStateChanged();
    }

    /// <summary>
    /// Barber declines the current request
    /// </summary>
    public void DeclineRequest()
    {
        if (_currentRequest?.State != RequestState.Pending) return;

        _currentRequest = null;
        NotifyStateChanged();
    }

    /// <summary>
    /// Complete the session (either party can trigger)
    /// </summary>
    public void CompleteSession()
    {
        if (_currentRequest == null) return;

        _currentRequest.State = RequestState.Completed;
        NotifyStateChanged();

        // Auto-reset after a brief moment (simulated)
        _ = Task.Delay(500).ContinueWith(_ => Reset());
    }

    /// <summary>
    /// Reset to idle state
    /// </summary>
    public void Reset()
    {
        _currentRequest = null;
        NotifyStateChanged();
    }

    /// <summary>
    /// Update barber location (for simulation)
    /// </summary>
    public void UpdateBarberLocation(Location location)
    {
        _currentRequest?.BarberLocation = location;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnStateChanged?.Invoke();
}
