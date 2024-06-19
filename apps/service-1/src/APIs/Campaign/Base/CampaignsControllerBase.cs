using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service_1.APIs;
using Service_1.APIs.Common;
using Service_1.APIs.Dtos;
using Service_1.APIs.Errors;

namespace Service_1.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class CampaignsControllerBase : ControllerBase
{
    protected readonly ICampaignsService _service;

    public CampaignsControllerBase(ICampaignsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Meta data about Campaign records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> CampaignsMeta(
        [FromQuery()] CampaignFindMany filter
    )
    {
        return Ok(await _service.CampaignsMeta(filter));
    }

    /// <summary>
    /// Create one Campaign
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<CampaignDto>> CreateCampaign(CampaignCreateInput input)
    {
        var campaign = await _service.CreateCampaign(input);

        return CreatedAtAction(nameof(Campaign), new { id = campaign.Id }, campaign);
    }

    /// <summary>
    /// Delete one Campaign
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteCampaign([FromRoute()] CampaignIdDto idDto)
    {
        try
        {
            await _service.DeleteCampaign(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Campaigns
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<CampaignDto>>> Campaigns(
        [FromQuery()] CampaignFindMany filter
    )
    {
        return Ok(await _service.Campaigns(filter));
    }

    /// <summary>
    /// Get one Campaign
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<CampaignDto>> Campaign([FromRoute()] CampaignIdDto idDto)
    {
        try
        {
            return await _service.Campaign(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Campaign
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateCampaign(
        [FromRoute()] CampaignIdDto idDto,
        [FromQuery()] CampaignUpdateInput campaignUpdateDto
    )
    {
        try
        {
            await _service.UpdateCampaign(idDto, campaignUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
