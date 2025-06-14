namespace PubSubCore;

public record NewsItem
{
    public required int Id { get; init; }
    public required string Title { get; init; }
    public string? Body { get; init; }
    public required DateTime PublishedAt { get; init; }
    public required int PublisherId { get; init; }
    public required string PublisherName { get; init; }
}