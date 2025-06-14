namespace PubSubCore;

public class Publisher
{
    private static int LastId = 0;

    public int Id { get; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public NewsFeed NewsFeed { get; }

    public Publisher(int id, string name, string? description = null)
    {
        Id = id;
        Name = name;
        Description = description;
        NewsFeed = new NewsFeed
        {
            Publisher = this,
            Contents = []
        };
    }

    public void Publish(string title, string? body = null)
    {
        NewsFeed.Contents.Add(new NewsItem
        {
            Id = ++LastId,
            Title = title,
            Body = body,
            PublishedAt = DateTime.UtcNow,
            PublisherId = Id,
            PublisherName = Name
        });
    }
}
