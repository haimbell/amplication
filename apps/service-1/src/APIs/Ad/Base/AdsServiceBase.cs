using Microsoft.EntityFrameworkCore;
using Service_1.APIs;
using Service_1.APIs.Common;
using Service_1.APIs.Dtos;
using Service_1.APIs.Errors;
using Service_1.APIs.Extensions;
using Service_1.Infrastructure;
using Service_1.Infrastructure.Models;

namespace Service_1.APIs;

public abstract class AdsServiceBase : IAdsService
{
    protected readonly Service_1DbContext _context;

    public AdsServiceBase(Service_1DbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Meta data about Ad records
    /// </summary>
    public async Task<MetadataDto> AdsMeta(AdFindMany findManyArgs)
    {
        var count = await _context.Ads.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Create one Ad
    /// </summary>
    public async Task<AdDto> CreateAd(AdCreateInput createDto)
    {
        var ad = new Ad { CreatedAt = createDto.CreatedAt, UpdatedAt = createDto.UpdatedAt };

        if (createDto.Id != null)
        {
            ad.Id = createDto.Id;
        }

        _context.Ads.Add(ad);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<Ad>(ad.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Ad
    /// </summary>
    public async Task DeleteAd(AdIdDto idDto)
    {
        var ad = await _context.Ads.FindAsync(idDto.Id);
        if (ad == null)
        {
            throw new NotFoundException();
        }

        _context.Ads.Remove(ad);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Ads
    /// </summary>
    public async Task<List<AdDto>> Ads(AdFindMany findManyArgs)
    {
        var ads = await _context
            .Ads.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return ads.ConvertAll(ad => ad.ToDto());
    }

    /// <summary>
    /// Get one Ad
    /// </summary>
    public async Task<AdDto> Ad(AdIdDto idDto)
    {
        var ads = await this.Ads(new AdFindMany { Where = new AdWhereInput { Id = idDto.Id } });
        var ad = ads.FirstOrDefault();
        if (ad == null)
        {
            throw new NotFoundException();
        }

        return ad;
    }

    /// <summary>
    /// Update one Ad
    /// </summary>
    public async Task UpdateAd(AdIdDto idDto, AdUpdateInput updateDto)
    {
        var ad = updateDto.ToModel(idDto);

        _context.Entry(ad).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Ads.Any(e => e.Id == ad.Id))
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
