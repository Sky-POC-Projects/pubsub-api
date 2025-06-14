using PubSubCore;

namespace PubSubAPI.Models;

public record PublisherResponse
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public List<NewsItem>? NewsFeed { get; init; }
}