using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service_1.APIs;
using Service_1.APIs.Common;
using Service_1.APIs.Dtos;
using Service_1.APIs.Errors;

namespace Service_1.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class ClicksControllerBase : ControllerBase
{
    protected readonly IClicksService _service;

    public ClicksControllerBase(IClicksService service)
    {
        _service = service;
    }

    /// <summary>
    /// Meta data about Click records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> ClicksMeta([FromQuery()] ClickFindMany filter)
    {
        return Ok(await _service.ClicksMeta(filter));
    }

    /// <summary>
    /// Create one Click
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<ClickDto>> CreateClick(ClickCreateInput input)
    {
        var click = await _service.CreateClick(input);

        return CreatedAtAction(nameof(Click), new { id = click.Id }, click);
    }

    /// <summary>
    /// Delete one Click
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteClick([FromRoute()] ClickIdDto idDto)
    {
        try
        {
            await _service.DeleteClick(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Clicks
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<ClickDto>>> Clicks([FromQuery()] ClickFindMany filter)
    {
        return Ok(await _service.Clicks(filter));
    }

    /// <summary>
    /// Get one Click
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<ClickDto>> Click([FromRoute()] ClickIdDto idDto)
    {
        try
        {
            return await _service.Click(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Click
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateClick(
        [FromRoute()] ClickIdDto idDto,
        [FromQuery()] ClickUpdateInput clickUpdateDto
    )
    {
        try
        {
            await _service.UpdateClick(idDto, clickUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
