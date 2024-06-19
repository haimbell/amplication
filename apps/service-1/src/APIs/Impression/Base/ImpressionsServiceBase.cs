using Microsoft.EntityFrameworkCore;
using Service_1.APIs;
using Service_1.APIs.Common;
using Service_1.APIs.Dtos;
using Service_1.APIs.Errors;
using Service_1.APIs.Extensions;
using Service_1.Infrastructure;
using Service_1.Infrastructure.Models;

namespace Service_1.APIs;

public abstract class ImpressionsServiceBase : IImpressionsService
{
    protected readonly Service_1DbContext _context;

    public ImpressionsServiceBase(Service_1DbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Impression
    /// </summary>
    public async Task<ImpressionDto> CreateImpression(ImpressionCreateInput createDto)
    {
        var impression = new Impression
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            impression.Id = createDto.Id;
        }

        _context.Impressions.Add(impression);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<Impression>(impression.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Impression
    /// </summary>
    public async Task DeleteImpression(ImpressionIdDto idDto)
    {
        var impression = await _context.Impressions.FindAsync(idDto.Id);
        if (impression == null)
        {
            throw new NotFoundException();
        }

        _context.Impressions.Remove(impression);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Impressions
    /// </summary>
    public async Task<List<ImpressionDto>> Impressions(ImpressionFindMany findManyArgs)
    {
        var impressions = await _context
            .Impressions.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return impressions.ConvertAll(impression => impression.ToDto());
    }

    /// <summary>
    /// Get one Impression
    /// </summary>
    public async Task<ImpressionDto> Impression(ImpressionIdDto idDto)
    {
        var impressions = await this.Impressions(
            new ImpressionFindMany { Where = new ImpressionWhereInput { Id = idDto.Id } }
        );
        var impression = impressions.FirstOrDefault();
        if (impression == null)
        {
            throw new NotFoundException();
        }

        return impression;
    }

    /// <summary>
    /// Meta data about Impression records
    /// </summary>
    public async Task<MetadataDto> ImpressionsMeta(ImpressionFindMany findManyArgs)
    {
        var count = await _context.Impressions.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Update one Impression
    /// </summary>
    public async Task UpdateImpression(ImpressionIdDto idDto, ImpressionUpdateInput updateDto)
    {
        var impression = updateDto.ToModel(idDto);

        _context.Entry(impression).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Impressions.Any(e => e.Id == impression.Id))
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
