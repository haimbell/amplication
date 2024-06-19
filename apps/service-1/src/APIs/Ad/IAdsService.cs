using Service_1.APIs.Common;
using Service_1.APIs.Dtos;

namespace Service_1.APIs;

public interface IAdsService
{
    /// <summary>
    /// Meta data about Ad records
    /// </summary>
    public Task<MetadataDto> AdsMeta(AdFindMany findManyArgs);

    /// <summary>
    /// Create one Ad
    /// </summary>
    public Task<AdDto> CreateAd(AdCreateInput adDto);

    /// <summary>
    /// Delete one Ad
    /// </summary>
    public Task DeleteAd(AdIdDto idDto);

    /// <summary>
    /// Find many Ads
    /// </summary>
    public Task<List<AdDto>> Ads(AdFindMany findManyArgs);

    /// <summary>
    /// Get one Ad
    /// </summary>
    public Task<AdDto> Ad(AdIdDto idDto);

    /// <summary>
    /// Update one Ad
    /// </summary>
    public Task UpdateAd(AdIdDto idDto, AdUpdateInput updateDto);
}
