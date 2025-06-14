namespace PubSubCore;

public class NewsFeed
{
    public required Publisher Publisher { get; set; }
    public required List<NewsItem> Contents { get; set; }
}