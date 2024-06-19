using Service_1.APIs.Dtos;
using Service_1.Infrastructure.Models;

namespace Service_1.APIs.Extensions;

public static class CampaignsExtensions
{
    public static CampaignDto ToDto(this Campaign model)
    {
        return new CampaignDto
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static Campaign ToModel(this CampaignUpdateInput updateDto, CampaignIdDto idDto)
    {
        var campaign = new Campaign { Id = idDto.Id };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            campaign.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            campaign.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return campaign;
    }
}
