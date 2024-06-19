using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service_1.APIs;
using Service_1.APIs.Common;
using Service_1.APIs.Dtos;
using Service_1.APIs.Errors;

namespace Service_1.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class PublishersControllerBase : ControllerBase
{
    protected readonly IPublishersService _service;

    public PublishersControllerBase(IPublishersService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Publisher
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<PublisherDto>> CreatePublisher(PublisherCreateInput input)
    {
        var publisher = await _service.CreatePublisher(input);

        return CreatedAtAction(nameof(Publisher), new { id = publisher.Id }, publisher);
    }

    /// <summary>
    /// Delete one Publisher
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeletePublisher([FromRoute()] PublisherIdDto idDto)
    {
        try
        {
            await _service.DeletePublisher(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Publishers
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<PublisherDto>>> Publishers(
        [FromQuery()] PublisherFindMany filter
    )
    {
        return Ok(await _service.Publishers(filter));
    }

    /// <summary>
    /// Get one Publisher
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<PublisherDto>> Publisher([FromRoute()] PublisherIdDto idDto)
    {
        try
        {
            return await _service.Publisher(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Meta data about Publisher records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> PublishersMeta(
        [FromQuery()] PublisherFindMany filter
    )
    {
        return Ok(await _service.PublishersMeta(filter));
    }

    /// <summary>
    /// Update one Publisher
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdatePublisher(
        [FromRoute()] PublisherIdDto idDto,
        [FromQuery()] PublisherUpdateInput publisherUpdateDto
    )
    {
        try
        {
            await _service.UpdatePublisher(idDto, publisherUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
