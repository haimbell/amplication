using Service_1.APIs.Dtos;
using Service_1.Infrastructure.Models;

namespace Service_1.APIs.Extensions;

public static class ImpressionsExtensions
{
    public static ImpressionDto ToDto(this Impression model)
    {
        return new ImpressionDto
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static Impression ToModel(this ImpressionUpdateInput updateDto, ImpressionIdDto idDto)
    {
        var impression = new Impression { Id = idDto.Id };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            impression.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            impression.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return impression;
    }
}
