using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service_1.APIs;
using Service_1.APIs.Common;
using Service_1.APIs.Dtos;
using Service_1.APIs.Errors;

namespace Service_1.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class ImpressionsControllerBase : ControllerBase
{
    protected readonly IImpressionsService _service;

    public ImpressionsControllerBase(IImpressionsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Impression
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<ImpressionDto>> CreateImpression(ImpressionCreateInput input)
    {
        var impression = await _service.CreateImpression(input);

        return CreatedAtAction(nameof(Impression), new { id = impression.Id }, impression);
    }

    /// <summary>
    /// Delete one Impression
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteImpression([FromRoute()] ImpressionIdDto idDto)
    {
        try
        {
            await _service.DeleteImpression(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Impressions
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<ImpressionDto>>> Impressions(
        [FromQuery()] ImpressionFindMany filter
    )
    {
        return Ok(await _service.Impressions(filter));
    }

    /// <summary>
    /// Get one Impression
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<ImpressionDto>> Impression([FromRoute()] ImpressionIdDto idDto)
    {
        try
        {
            return await _service.Impression(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Meta data about Impression records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> ImpressionsMeta(
        [FromQuery()] ImpressionFindMany filter
    )
    {
        return Ok(await _service.ImpressionsMeta(filter));
    }

    /// <summary>
    /// Update one Impression
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateImpression(
        [FromRoute()] ImpressionIdDto idDto,
        [FromQuery()] ImpressionUpdateInput impressionUpdateDto
    )
    {
        try
        {
            await _service.UpdateImpression(idDto, impressionUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
