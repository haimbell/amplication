using Service_1.APIs.Common;
using Service_1.APIs.Dtos;

namespace Service_1.APIs;

public interface ICampaignsService
{
    /// <summary>
    /// Meta data about Campaign records
    /// </summary>
    public Task<MetadataDto> CampaignsMeta(CampaignFindMany findManyArgs);

    /// <summary>
    /// Create one Campaign
    /// </summary>
    public Task<CampaignDto> CreateCampaign(CampaignCreateInput campaignDto);

    /// <summary>
    /// Delete one Campaign
    /// </summary>
    public Task DeleteCampaign(CampaignIdDto idDto);

    /// <summary>
    /// Find many Campaigns
    /// </summary>
    public Task<List<CampaignDto>> Campaigns(CampaignFindMany findManyArgs);

    /// <summary>
    /// Get one Campaign
    /// </summary>
    public Task<CampaignDto> Campaign(CampaignIdDto idDto);

    /// <summary>
    /// Update one Campaign
    /// </summary>
    public Task UpdateCampaign(CampaignIdDto idDto, CampaignUpdateInput updateDto);
}
