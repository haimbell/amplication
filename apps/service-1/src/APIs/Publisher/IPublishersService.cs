using Service_1.APIs.Common;
using Service_1.APIs.Dtos;

namespace Service_1.APIs;

public interface IPublishersService
{
    /// <summary>
    /// Create one Publisher
    /// </summary>
    public Task<PublisherDto> CreatePublisher(PublisherCreateInput publisherDto);

    /// <summary>
    /// Delete one Publisher
    /// </summary>
    public Task DeletePublisher(PublisherIdDto idDto);

    /// <summary>
    /// Find many Publishers
    /// </summary>
    public Task<List<PublisherDto>> Publishers(PublisherFindMany findManyArgs);

    /// <summary>
    /// Get one Publisher
    /// </summary>
    public Task<PublisherDto> Publisher(PublisherIdDto idDto);

    /// <summary>
    /// Meta data about Publisher records
    /// </summary>
    public Task<MetadataDto> PublishersMeta(PublisherFindMany findManyArgs);

    /// <summary>
    /// Update one Publisher
    /// </summary>
    public Task UpdatePublisher(PublisherIdDto idDto, PublisherUpdateInput updateDto);
}
