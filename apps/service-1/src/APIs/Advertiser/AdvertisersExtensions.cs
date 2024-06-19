using Service_1.APIs.Dtos;
using Service_1.Infrastructure.Models;

namespace Service_1.APIs.Extensions;

public static class AdvertisersExtensions
{
    public static AdvertiserDto ToDto(this Advertiser model)
    {
        return new AdvertiserDto
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static Advertiser ToModel(this AdvertiserUpdateInput updateDto, AdvertiserIdDto idDto)
    {
        var advertiser = new Advertiser { Id = idDto.Id };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            advertiser.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            advertiser.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return advertiser;
    }
}
