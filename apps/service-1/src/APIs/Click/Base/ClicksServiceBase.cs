using Microsoft.EntityFrameworkCore;
using Service_1.APIs;
using Service_1.APIs.Common;
using Service_1.APIs.Dtos;
using Service_1.APIs.Errors;
using Service_1.APIs.Extensions;
using Service_1.Infrastructure;
using Service_1.Infrastructure.Models;

namespace Service_1.APIs;

public abstract class ClicksServiceBase : IClicksService
{
    protected readonly Service_1DbContext _context;

    public ClicksServiceBase(Service_1DbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Meta data about Click records
    /// </summary>
    public async Task<MetadataDto> ClicksMeta(ClickFindMany findManyArgs)
    {
        var count = await _context.Clicks.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Create one Click
    /// </summary>
    public async Task<ClickDto> CreateClick(ClickCreateInput createDto)
    {
        var click = new Click { CreatedAt = createDto.CreatedAt, UpdatedAt = createDto.UpdatedAt };

        if (createDto.Id != null)
        {
            click.Id = createDto.Id;
        }

        _context.Clicks.Add(click);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<Click>(click.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Click
    /// </summary>
    public async Task DeleteClick(ClickIdDto idDto)
    {
        var click = await _context.Clicks.FindAsync(idDto.Id);
        if (click == null)
        {
            throw new NotFoundException();
        }

        _context.Clicks.Remove(click);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Clicks
    /// </summary>
    public async Task<List<ClickDto>> Clicks(ClickFindMany findManyArgs)
    {
        var clicks = await _context
            .Clicks.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return clicks.ConvertAll(click => click.ToDto());
    }

    /// <summary>
    /// Get one Click
    /// </summary>
    public async Task<ClickDto> Click(ClickIdDto idDto)
    {
        var clicks = await this.Clicks(
            new ClickFindMany { Where = new ClickWhereInput { Id = idDto.Id } }
        );
        var click = clicks.FirstOrDefault();
        if (click == null)
        {
            throw new NotFoundException();
        }

        return click;
    }

    /// <summary>
    /// Update one Click
    /// </summary>
    public async Task UpdateClick(ClickIdDto idDto, ClickUpdateInput updateDto)
    {
        var click = updateDto.ToModel(idDto);

        _context.Entry(click).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Clicks.Any(e => e.Id == click.Id))
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
