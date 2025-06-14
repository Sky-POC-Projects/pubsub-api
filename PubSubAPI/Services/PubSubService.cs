using PubSubCore;

namespace PubSubAPI.Services;

public class PubSubService
{
    private readonly Dictionary<int, Publisher> Publishers = [];
    private readonly Dictionary<int, Subscriber> Subscribers = [];

    public PubSubService() { }

    public Publisher GetPublisher(int publisherId)
    {
        if (publisherId <= 0)
        {
            throw new ArgumentException("Publisher ID must be greater than zero");
        }
        if (!Publishers.TryGetValue(publisherId, out Publisher? value))
        {
            throw new KeyNotFoundException($"Publisher with ID {publisherId} not found");
        }
        return value;
    }

    public List<Publisher> GetAllPublishers()
    {
        return [.. Publishers.Values];
    }

    public Publisher CreatePublisher(string name, string? description = null)
    {
        int newId = Publishers.Count + 1;
        var publisher = Publishers.Values.FirstOrDefault(pub => pub.Name == name, new Publisher(newId, name, description));
        return Publishers[publisher.Id] = publisher;
    }

    public void Publish(int publisherId, string topic, string message)
    {
        if (string.IsNullOrEmpty(topic))
        {
            throw new ArgumentException("Topic and message cannot be null or empty");
        }

        Publishers[publisherId].Publish(topic, message);
    }

    public Subscriber GetSubscriber(int subscriberId)
    {
        if (subscriberId <= 0)
        {
            throw new ArgumentException("Subscriber ID must be greater than zero");
        }
        if (!Subscribers.TryGetValue(subscriberId, out Subscriber? value))
        {
            throw new KeyNotFoundException($"Subscriber with ID {subscriberId} not found");
        }
        return value;
    }

    public Subscriber CreateSubscriber(string name)
    {
        int newId = Subscribers.Count + 1;
        var subscriber = Subscribers.Values.FirstOrDefault(sub => sub.Name == name, new Subscriber(newId, name));
        return Subscribers[subscriber.Id] = subscriber;
    }

    public void Subscribe(int subscriberId, int publisherId)
    {
        if (subscriberId <= 0 || publisherId <= 0)
        {
            throw new ArgumentException("Subscriber ID and Publisher ID must be greater than zero");
        }

        var subscriber = GetSubscriber(subscriberId);
        var publisher = GetPublisher(publisherId);
        subscriber.Subscribe(publisher);
    }

    public void Unsubscribe(int subscriberId, int publisherId)
    {
        if (subscriberId <= 0 || publisherId <= 0)
        {
            throw new ArgumentException("Subscriber ID and Publisher ID must be greater than zero");
        }

        var subscriber = GetSubscriber(subscriberId);
        var publisher = GetPublisher(publisherId);
        subscriber.Unsubscribe(publisher);
    }
}