namespace PubSubAPI.Models;

public record PublisherRequest
{
    /// <summary>
    /// Gets or sets the name of the publisher.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the publisher.
    /// </summary>
    public string Description { get; set; } = string.Empty;
}