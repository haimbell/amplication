using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service_1.APIs;
using Service_1.APIs.Common;
using Service_1.APIs.Dtos;
using Service_1.APIs.Errors;

namespace Service_1.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class AdvertisersControllerBase : ControllerBase
{
    protected readonly IAdvertisersService _service;

    public AdvertisersControllerBase(IAdvertisersService service)
    {
        _service = service;
    }

    /// <summary>
    /// Meta data about Advertiser records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> AdvertisersMeta(
        [FromQuery()] AdvertiserFindMany filter
    )
    {
        return Ok(await _service.AdvertisersMeta(filter));
    }

    /// <summary>
    /// Create one Advertiser
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<AdvertiserDto>> CreateAdvertiser(AdvertiserCreateInput input)
    {
        var advertiser = await _service.CreateAdvertiser(input);

        return CreatedAtAction(nameof(Advertiser), new { id = advertiser.Id }, advertiser);
    }

    /// <summary>
    /// Delete one Advertiser
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteAdvertiser([FromRoute()] AdvertiserIdDto idDto)
    {
        try
        {
            await _service.DeleteAdvertiser(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Advertisers
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<AdvertiserDto>>> Advertisers(
        [FromQuery()] AdvertiserFindMany filter
    )
    {
        return Ok(await _service.Advertisers(filter));
    }

    /// <summary>
    /// Get one Advertiser
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<AdvertiserDto>> Advertiser([FromRoute()] AdvertiserIdDto idDto)
    {
        try
        {
            return await _service.Advertiser(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Advertiser
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateAdvertiser(
        [FromRoute()] AdvertiserIdDto idDto,
        [FromQuery()] AdvertiserUpdateInput advertiserUpdateDto
    )
    {
        try
        {
            await _service.UpdateAdvertiser(idDto, advertiserUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
