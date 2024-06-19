using Service_1.APIs.Common;
using Service_1.APIs.Dtos;

namespace Service_1.APIs;

public interface IImpressionsService
{
    /// <summary>
    /// Create one Impression
    /// </summary>
    public Task<ImpressionDto> CreateImpression(ImpressionCreateInput impressionDto);

    /// <summary>
    /// Delete one Impression
    /// </summary>
    public Task DeleteImpression(ImpressionIdDto idDto);

    /// <summary>
    /// Find many Impressions
    /// </summary>
    public Task<List<ImpressionDto>> Impressions(ImpressionFindMany findManyArgs);

    /// <summary>
    /// Get one Impression
    /// </summary>
    public Task<ImpressionDto> Impression(ImpressionIdDto idDto);

    /// <summary>
    /// Meta data about Impression records
    /// </summary>
    public Task<MetadataDto> ImpressionsMeta(ImpressionFindMany findManyArgs);

    /// <summary>
    /// Update one Impression
    /// </summary>
    public Task UpdateImpression(ImpressionIdDto idDto, ImpressionUpdateInput updateDto);
}
