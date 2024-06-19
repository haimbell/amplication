using Microsoft.EntityFrameworkCore;
using Service_1.APIs;
using Service_1.APIs.Common;
using Service_1.APIs.Dtos;
using Service_1.APIs.Errors;
using Service_1.APIs.Extensions;
using Service_1.Infrastructure;
using Service_1.Infrastructure.Models;

namespace Service_1.APIs;

public abstract class PublishersServiceBase : IPublishersService
{
    protected readonly Service_1DbContext _context;

    public PublishersServiceBase(Service_1DbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Publisher
    /// </summary>
    public async Task<PublisherDto> CreatePublisher(PublisherCreateInput createDto)
    {
        var publisher = new Publisher
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            publisher.Id = createDto.Id;
        }

        _context.Publishers.Add(publisher);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<Publisher>(publisher.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Publisher
    /// </summary>
    public async Task DeletePublisher(PublisherIdDto idDto)
    {
        var publisher = await _context.Publishers.FindAsync(idDto.Id);
        if (publisher == null)
        {
            throw new NotFoundException();
        }

        _context.Publishers.Remove(publisher);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Publishers
    /// </summary>
    public async Task<List<PublisherDto>> Publishers(PublisherFindMany findManyArgs)
    {
        var publishers = await _context
            .Publishers.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return publishers.ConvertAll(publisher => publisher.ToDto());
    }

    /// <summary>
    /// Get one Publisher
    /// </summary>
    public async Task<PublisherDto> Publisher(PublisherIdDto idDto)
    {
        var publishers = await this.Publishers(
            new PublisherFindMany { Where = new PublisherWhereInput { Id = idDto.Id } }
        );
        var publisher = publishers.FirstOrDefault();
        if (publisher == null)
        {
            throw new NotFoundException();
        }

        return publisher;
    }

    /// <summary>
    /// Meta data about Publisher records
    /// </summary>
    public async Task<MetadataDto> PublishersMeta(PublisherFindMany findManyArgs)
    {
        var count = await _context.Publishers.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Update one Publisher
    /// </summary>
    public async Task UpdatePublisher(PublisherIdDto idDto, PublisherUpdateInput updateDto)
    {
        var publisher = updateDto.ToModel(idDto);

        _context.Entry(publisher).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Publishers.Any(e => e.Id == publisher.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
