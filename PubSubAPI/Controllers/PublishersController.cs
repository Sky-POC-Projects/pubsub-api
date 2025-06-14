using Microsoft.AspNetCore.Mvc;
using PubSubAPI.Models;
using PubSubAPI.Services;

namespace PubSubAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PublishersController(PubSubService publisherService) : ControllerBase
{
    private readonly PubSubService _publisherService = publisherService;

    [HttpPost("")]
    public IActionResult Index([FromBody] PublisherRequest request)
    {
        if (request == null || string.IsNullOrEmpty(request.Name))
        {
            return BadRequest("Invalid  request");
        }
        
        var publisher = _publisherService.CreatePublisher(request.Name, request.Description);
        if (publisher == null)
        {
            return BadRequest("Failed to create publisher");
        }

        return Ok(new PublisherResponse
        {
            Id = publisher.Id,
            Name = publisher.Name,
            NewsFeed = [.. publisher.NewsFeed.Contents]
        });
    }

    [HttpGet("{id}")]
    public IActionResult GetPublisher(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid publisher ID");
        }
        
        try
        {
            var publisher = _publisherService.GetPublisher(id);
            return Ok(new PublisherResponse()
            {
                Id = publisher.Id,
                Name = publisher.Name,
                NewsFeed = [.. publisher.NewsFeed.Contents]
            });
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Publisher with ID {id} not found");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("")]
    public IActionResult GetAllPublishers()
    {
        var publishers = _publisherService.GetAllPublishers();
        return Ok(publishers.Select(p => new PublisherResponse
        {
            Id = p.Id,
            Name = p.Name,
        }));
    }

    [HttpPost("{id}/publish")]
    public IActionResult Publish(int id, [FromBody] PublishRequest request)
    {
        if (request == null || string.IsNullOrEmpty(request.Title) || string.IsNullOrEmpty(request.Body))
        {
            return BadRequest("Invalid request");
        }

        _publisherService.Publish(id, request.Title, request.Body);
        return Ok("Message published successfully");
    }
}