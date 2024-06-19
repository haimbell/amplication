using Service_1.APIs.Dtos;
using Service_1.Infrastructure.Models;

namespace Service_1.APIs.Extensions;

public static class AdsExtensions
{
    public static AdDto ToDto(this Ad model)
    {
        return new AdDto
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static Ad ToModel(this AdUpdateInput updateDto, AdIdDto idDto)
    {
        var ad = new Ad { Id = idDto.Id };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            ad.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            ad.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return ad;
    }
}
