using Service_1.APIs.Dtos;
using Service_1.Infrastructure.Models;

namespace Service_1.APIs.Extensions;

public static class ClicksExtensions
{
    public static ClickDto ToDto(this Click model)
    {
        return new ClickDto
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static Click ToModel(this ClickUpdateInput updateDto, ClickIdDto idDto)
    {
        var click = new Click { Id = idDto.Id };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            click.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            click.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return click;
    }
}
