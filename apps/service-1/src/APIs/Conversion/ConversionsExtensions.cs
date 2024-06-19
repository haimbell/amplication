using Service_1.APIs.Dtos;
using Service_1.Infrastructure.Models;

namespace Service_1.APIs.Extensions;

public static class ConversionsExtensions
{
    public static ConversionDto ToDto(this Conversion model)
    {
        return new ConversionDto
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static Conversion ToModel(this ConversionUpdateInput updateDto, ConversionIdDto idDto)
    {
        var conversion = new Conversion { Id = idDto.Id };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            conversion.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            conversion.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return conversion;
    }
}
