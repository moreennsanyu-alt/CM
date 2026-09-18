using Prism.Events;

namespace ClinicManager.Win.Core.Events;

public sealed class LogoutRequestedEvent : PubSubEvent<LogoutRequestedEventArgs>
{
}

public sealed class LogoutRequestedEventArgs
{
    public bool Cancel { get; set; }
    public string? CancellationReason { get; set; }
}
