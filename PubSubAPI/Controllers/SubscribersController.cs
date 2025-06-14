using Microsoft.AspNetCore.Mvc;
using PubSubAPI.Models;
using PubSubAPI.Services;
using PubSubCore;

namespace PubSubAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubscribersController(PubSubService pubSubService) : ControllerBase
{
    private readonly PubSubService PubSubService = pubSubService;

    [HttpPost("")]
    public IActionResult Index([FromBody] SubscriberRequest request)
    {
        if (request == null || string.IsNullOrEmpty(request.Name))
        {
            return BadRequest("Invalid subscriber request");
        }

        try
        {
            var subscriber = PubSubService.CreateSubscriber(request.Name);
            return Ok(new SubscriberResponse
            {
                Id = subscriber.Id,
                Name = subscriber.Name,
                SubscribedPublishers = [.. subscriber.Subscriptions.Select(sub => new PublisherResponse { Id = sub.Publisher.Id, Name = sub.Publisher.Name })]
            });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetSubscriber(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid subscriber ID");
        }

        try
        {
            var subscriber = PubSubService.GetSubscriber(id);
            return Ok(new SubscriberResponse
            {
                Id = subscriber.Id,
                Name = subscriber.Name,
                SubscribedPublishers = [.. subscriber.Subscriptions.Select(sub => new PublisherResponse { Id = sub.Publisher.Id, Name = sub.Publisher.Name })]
            });
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Subscriber with ID {id} not found");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{subscriberId}/subscribe/{publisherId}")]
    public IActionResult Subscribe(int subscriberId, int publisherId)
    {
        if (subscriberId <= 0 || publisherId <= 0)
        {
            return BadRequest("Subscriber ID and Publisher ID must be greater than zero");
        }

        try
        {
            PubSubService.Subscribe(subscriberId, publisherId);
            return Ok($"Subscribed to publisher with ID {publisherId} successfully.");
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Publisher with ID {publisherId} not found");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{subscriberId}/subscribe/{publisherId}")]
    public IActionResult Unsubscribe(int subscriberId, int publisherId)
    {
        if (subscriberId <= 0 || publisherId <= 0)
        {
            return BadRequest("Subscriber ID and Publisher ID must be greater than zero");
        }

        try
        {
            PubSubService.Unsubscribe(subscriberId, publisherId);
            return Ok($"Unsubscribed from publisher with ID {publisherId} successfully.");
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Publisher with ID {publisherId} not found");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{subscriberId}/feed")]
    public ActionResult<List<NewsItem>?> GetFeed(int subscriberId, int count = 10)
    {
        if (subscriberId <= 0 || count <= 0)
        {
            return BadRequest("Subscriber ID and count must be greater than zero");
        }

        try
        {
            var subscriber = PubSubService.GetSubscriber(subscriberId);
            var newsFeed = subscriber.GetFeed(count);
            return Ok(newsFeed);
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Subscriber with ID {subscriberId} not found");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}