using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service_1.APIs;
using Service_1.APIs.Common;
using Service_1.APIs.Dtos;
using Service_1.APIs.Errors;

namespace Service_1.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class ConversionsControllerBase : ControllerBase
{
    protected readonly IConversionsService _service;

    public ConversionsControllerBase(IConversionsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Meta data about Conversion records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> ConversionsMeta(
        [FromQuery()] ConversionFindMany filter
    )
    {
        return Ok(await _service.ConversionsMeta(filter));
    }

    /// <summary>
    /// Create one Conversion
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<ConversionDto>> CreateConversion(ConversionCreateInput input)
    {
        var conversion = await _service.CreateConversion(input);

        return CreatedAtAction(nameof(Conversion), new { id = conversion.Id }, conversion);
    }

    /// <summary>
    /// Delete one Conversion
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteConversion([FromRoute()] ConversionIdDto idDto)
    {
        try
        {
            await _service.DeleteConversion(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Conversions
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<ConversionDto>>> Conversions(
        [FromQuery()] ConversionFindMany filter
    )
    {
        return Ok(await _service.Conversions(filter));
    }

    /// <summary>
    /// Get one Conversion
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<ConversionDto>> Conversion([FromRoute()] ConversionIdDto idDto)
    {
        try
        {
            return await _service.Conversion(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Conversion
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateConversion(
        [FromRoute()] ConversionIdDto idDto,
        [FromQuery()] ConversionUpdateInput conversionUpdateDto
    )
    {
        try
        {
            await _service.UpdateConversion(idDto, conversionUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
