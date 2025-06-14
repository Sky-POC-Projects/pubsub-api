namespace PubSubCore;

public class Subscriber(int id, string name)
{
    public int Id { get; } = id;
    public string Name { get; set; } = name;
    public List<NewsFeed> Subscriptions { get; set; } = [];

    public void Subscribe(Publisher publisher)
    {
        if (!Subscriptions.Contains(publisher.NewsFeed))
        {
            Subscriptions.Add(publisher.NewsFeed);
        }
    }

    public void Unsubscribe(Publisher publisher)
    {
        Subscriptions.Remove(publisher.NewsFeed);
    }

    public List<NewsItem> GetFeed(int count)
    {
        var feed = new List<NewsItem>();
        var newsStacks = new List<Stack<NewsItem>>(Subscriptions.Count);
        
        foreach (var sub in Subscriptions) {
            newsStacks.Add(new Stack<NewsItem>(sub.Contents.Skip(sub.Contents.Count - count)));
        }

        while (count > 0)
        {
            if (newsStacks.All(stack => stack.Count == 0)) {
                // No news left to read
                break;
            }

            Stack<NewsItem>? latestNewsStack = null;
            foreach (var stack in newsStacks)
            {
                if (latestNewsStack == null || (stack.Count > 0 && stack.Peek().Id > latestNewsStack.Peek().Id))
                {
                    latestNewsStack = stack;
                }
            }

            if (latestNewsStack == null)
            {
                throw new Exception("News stack should not be null here.");
            }

            feed.Add(latestNewsStack.Pop());
            count--;
        }

        return feed;
    }
}
