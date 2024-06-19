using Microsoft.EntityFrameworkCore;
using Service_1.APIs;
using Service_1.APIs.Common;
using Service_1.APIs.Dtos;
using Service_1.APIs.Errors;
using Service_1.APIs.Extensions;
using Service_1.Infrastructure;
using Service_1.Infrastructure.Models;

namespace Service_1.APIs;

public abstract class ConversionsServiceBase : IConversionsService
{
    protected readonly Service_1DbContext _context;

    public ConversionsServiceBase(Service_1DbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Meta data about Conversion records
    /// </summary>
    public async Task<MetadataDto> ConversionsMeta(ConversionFindMany findManyArgs)
    {
        var count = await _context.Conversions.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Create one Conversion
    /// </summary>
    public async Task<ConversionDto> CreateConversion(ConversionCreateInput createDto)
    {
        var conversion = new Conversion
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            conversion.Id = createDto.Id;
        }

        _context.Conversions.Add(conversion);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<Conversion>(conversion.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Conversion
    /// </summary>
    public async Task DeleteConversion(ConversionIdDto idDto)
    {
        var conversion = await _context.Conversions.FindAsync(idDto.Id);
        if (conversion == null)
        {
            throw new NotFoundException();
        }

        _context.Conversions.Remove(conversion);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Conversions
    /// </summary>
    public async Task<List<ConversionDto>> Conversions(ConversionFindMany findManyArgs)
    {
        var conversions = await _context
            .Conversions.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return conversions.ConvertAll(conversion => conversion.ToDto());
    }

    /// <summary>
    /// Get one Conversion
    /// </summary>
    public async Task<ConversionDto> Conversion(ConversionIdDto idDto)
    {
        var conversions = await this.Conversions(
            new ConversionFindMany { Where = new ConversionWhereInput { Id = idDto.Id } }
        );
        var conversion = conversions.FirstOrDefault();
        if (conversion == null)
        {
            throw new NotFoundException();
        }

        return conversion;
    }

    /// <summary>
    /// Update one Conversion
    /// </summary>
    public async Task UpdateConversion(ConversionIdDto idDto, ConversionUpdateInput updateDto)
    {
        var conversion = updateDto.ToModel(idDto);

        _context.Entry(conversion).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Conversions.Any(e => e.Id == conversion.Id))
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
