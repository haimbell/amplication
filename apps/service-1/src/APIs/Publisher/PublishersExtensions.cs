using Service_1.APIs.Dtos;
using Service_1.Infrastructure.Models;

namespace Service_1.APIs.Extensions;

public static class PublishersExtensions
{
    public static PublisherDto ToDto(this Publisher model)
    {
        return new PublisherDto
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static Publisher ToModel(this PublisherUpdateInput updateDto, PublisherIdDto idDto)
    {
        var publisher = new Publisher { Id = idDto.Id };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            publisher.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            publisher.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return publisher;
    }
}
