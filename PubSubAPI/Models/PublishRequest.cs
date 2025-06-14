namespace PubSubAPI.Models;

public record PublishRequest
{
    /// <summary>
    /// Gets or sets the title of the message.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the body of the message.
    /// </summary>
    public string Body { get; set; } = string.Empty;
}