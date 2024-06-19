using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service_1.APIs;
using Service_1.APIs.Common;
using Service_1.APIs.Dtos;
using Service_1.APIs.Errors;

namespace Service_1.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class AdsControllerBase : ControllerBase
{
    protected readonly IAdsService _service;

    public AdsControllerBase(IAdsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Meta data about Ad records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> AdsMeta([FromQuery()] AdFindMany filter)
    {
        return Ok(await _service.AdsMeta(filter));
    }

    /// <summary>
    /// Create one Ad
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<AdDto>> CreateAd(AdCreateInput input)
    {
        var ad = await _service.CreateAd(input);

        return CreatedAtAction(nameof(Ad), new { id = ad.Id }, ad);
    }

    /// <summary>
    /// Delete one Ad
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteAd([FromRoute()] AdIdDto idDto)
    {
        try
        {
            await _service.DeleteAd(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Ads
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<AdDto>>> Ads([FromQuery()] AdFindMany filter)
    {
        return Ok(await _service.Ads(filter));
    }

    /// <summary>
    /// Get one Ad
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<AdDto>> Ad([FromRoute()] AdIdDto idDto)
    {
        try
        {
            return await _service.Ad(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Ad
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateAd(
        [FromRoute()] AdIdDto idDto,
        [FromQuery()] AdUpdateInput adUpdateDto
    )
    {
        try
        {
            await _service.UpdateAd(idDto, adUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
