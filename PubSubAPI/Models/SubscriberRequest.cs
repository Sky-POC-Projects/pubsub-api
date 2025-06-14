namespace PubSubAPI.Models;

public record SubscriberRequest
{
    /// <summary>
    /// Gets or sets the name of the subscriber.
    /// </summary>
    public string? Name { get; init; }
}