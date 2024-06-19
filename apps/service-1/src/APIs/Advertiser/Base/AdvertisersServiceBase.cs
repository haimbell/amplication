using Microsoft.EntityFrameworkCore;
using Service_1.APIs;
using Service_1.APIs.Common;
using Service_1.APIs.Dtos;
using Service_1.APIs.Errors;
using Service_1.APIs.Extensions;
using Service_1.Infrastructure;
using Service_1.Infrastructure.Models;

namespace Service_1.APIs;

public abstract class AdvertisersServiceBase : IAdvertisersService
{
    protected readonly Service_1DbContext _context;

    public AdvertisersServiceBase(Service_1DbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Meta data about Advertiser records
    /// </summary>
    public async Task<MetadataDto> AdvertisersMeta(AdvertiserFindMany findManyArgs)
    {
        var count = await _context.Advertisers.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Create one Advertiser
    /// </summary>
    public async Task<AdvertiserDto> CreateAdvertiser(AdvertiserCreateInput createDto)
    {
        var advertiser = new Advertiser
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            advertiser.Id = createDto.Id;
        }

        _context.Advertisers.Add(advertiser);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<Advertiser>(advertiser.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Advertiser
    /// </summary>
    public async Task DeleteAdvertiser(AdvertiserIdDto idDto)
    {
        var advertiser = await _context.Advertisers.FindAsync(idDto.Id);
        if (advertiser == null)
        {
            throw new NotFoundException();
        }

        _context.Advertisers.Remove(advertiser);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Advertisers
    /// </summary>
    public async Task<List<AdvertiserDto>> Advertisers(AdvertiserFindMany findManyArgs)
    {
        var advertisers = await _context
            .Advertisers.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return advertisers.ConvertAll(advertiser => advertiser.ToDto());
    }

    /// <summary>
    /// Get one Advertiser
    /// </summary>
    public async Task<AdvertiserDto> Advertiser(AdvertiserIdDto idDto)
    {
        var advertisers = await this.Advertisers(
            new AdvertiserFindMany { Where = new AdvertiserWhereInput { Id = idDto.Id } }
        );
        var advertiser = advertisers.FirstOrDefault();
        if (advertiser == null)
        {
            throw new NotFoundException();
        }

        return advertiser;
    }

    /// <summary>
    /// Update one Advertiser
    /// </summary>
    public async Task UpdateAdvertiser(AdvertiserIdDto idDto, AdvertiserUpdateInput updateDto)
    {
        var advertiser = updateDto.ToModel(idDto);

        _context.Entry(advertiser).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Advertisers.Any(e => e.Id == advertiser.Id))
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
