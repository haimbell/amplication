using Service_1.APIs.Common;
using Service_1.APIs.Dtos;

namespace Service_1.APIs;

public interface IClicksService
{
    /// <summary>
    /// Meta data about Click records
    /// </summary>
    public Task<MetadataDto> ClicksMeta(ClickFindMany findManyArgs);

    /// <summary>
    /// Create one Click
    /// </summary>
    public Task<ClickDto> CreateClick(ClickCreateInput clickDto);

    /// <summary>
    /// Delete one Click
    /// </summary>
    public Task DeleteClick(ClickIdDto idDto);

    /// <summary>
    /// Find many Clicks
    /// </summary>
    public Task<List<ClickDto>> Clicks(ClickFindMany findManyArgs);

    /// <summary>
    /// Get one Click
    /// </summary>
    public Task<ClickDto> Click(ClickIdDto idDto);

    /// <summary>
    /// Update one Click
    /// </summary>
    public Task UpdateClick(ClickIdDto idDto, ClickUpdateInput updateDto);
}
