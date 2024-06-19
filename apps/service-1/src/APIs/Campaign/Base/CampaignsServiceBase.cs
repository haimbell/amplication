using Microsoft.EntityFrameworkCore;
using Service_1.APIs;
using Service_1.APIs.Common;
using Service_1.APIs.Dtos;
using Service_1.APIs.Errors;
using Service_1.APIs.Extensions;
using Service_1.Infrastructure;
using Service_1.Infrastructure.Models;

namespace Service_1.APIs;

public abstract class CampaignsServiceBase : ICampaignsService
{
    protected readonly Service_1DbContext _context;

    public CampaignsServiceBase(Service_1DbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Meta data about Campaign records
    /// </summary>
    public async Task<MetadataDto> CampaignsMeta(CampaignFindMany findManyArgs)
    {
        var count = await _context.Campaigns.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Create one Campaign
    /// </summary>
    public async Task<CampaignDto> CreateCampaign(CampaignCreateInput createDto)
    {
        var campaign = new Campaign
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            campaign.Id = createDto.Id;
        }

        _context.Campaigns.Add(campaign);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<Campaign>(campaign.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Campaign
    /// </summary>
    public async Task DeleteCampaign(CampaignIdDto idDto)
    {
        var campaign = await _context.Campaigns.FindAsync(idDto.Id);
        if (campaign == null)
        {
            throw new NotFoundException();
        }

        _context.Campaigns.Remove(campaign);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Campaigns
    /// </summary>
    public async Task<List<CampaignDto>> Campaigns(CampaignFindMany findManyArgs)
    {
        var campaigns = await _context
            .Campaigns.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return campaigns.ConvertAll(campaign => campaign.ToDto());
    }

    /// <summary>
    /// Get one Campaign
    /// </summary>
    public async Task<CampaignDto> Campaign(CampaignIdDto idDto)
    {
        var campaigns = await this.Campaigns(
            new CampaignFindMany { Where = new CampaignWhereInput { Id = idDto.Id } }
        );
        var campaign = campaigns.FirstOrDefault();
        if (campaign == null)
        {
            throw new NotFoundException();
        }

        return campaign;
    }

    /// <summary>
    /// Update one Campaign
    /// </summary>
    public async Task UpdateCampaign(CampaignIdDto idDto, CampaignUpdateInput updateDto)
    {
        var campaign = updateDto.ToModel(idDto);

        _context.Entry(campaign).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Campaigns.Any(e => e.Id == campaign.Id))
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
