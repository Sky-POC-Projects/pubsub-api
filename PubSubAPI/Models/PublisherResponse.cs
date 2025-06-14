using PubSubCore;

namespace PubSubAPI.Models;

public record PublisherResponse
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public List<NewsItem>? NewsFeed { get; init; }
}