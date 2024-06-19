using Service_1.APIs.Common;
using Service_1.APIs.Dtos;

namespace Service_1.APIs;

public interface IAdvertisersService
{
    /// <summary>
    /// Meta data about Advertiser records
    /// </summary>
    public Task<MetadataDto> AdvertisersMeta(AdvertiserFindMany findManyArgs);

    /// <summary>
    /// Create one Advertiser
    /// </summary>
    public Task<AdvertiserDto> CreateAdvertiser(AdvertiserCreateInput advertiserDto);

    /// <summary>
    /// Delete one Advertiser
    /// </summary>
    public Task DeleteAdvertiser(AdvertiserIdDto idDto);

    /// <summary>
    /// Find many Advertisers
    /// </summary>
    public Task<List<AdvertiserDto>> Advertisers(AdvertiserFindMany findManyArgs);

    /// <summary>
    /// Get one Advertiser
    /// </summary>
    public Task<AdvertiserDto> Advertiser(AdvertiserIdDto idDto);

    /// <summary>
    /// Update one Advertiser
    /// </summary>
    public Task UpdateAdvertiser(AdvertiserIdDto idDto, AdvertiserUpdateInput updateDto);
}
