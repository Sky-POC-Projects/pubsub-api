namespace PubSubAPI.Models;

public record SubscriberResponse
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public required List<PublisherRequest> SubscribedPublishers { get; init; }
}