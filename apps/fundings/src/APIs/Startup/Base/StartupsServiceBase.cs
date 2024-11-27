using Fundings.APIs;
using Fundings.APIs.Common;
using Fundings.APIs.Dtos;
using Fundings.APIs.Errors;
using Fundings.APIs.Extensions;
using Fundings.Infrastructure;
using Fundings.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Fundings.APIs;

public abstract class StartupsServiceBase : IStartupsService
{
    protected readonly FundingsDbContext _context;

    public StartupsServiceBase(FundingsDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Startup
    /// </summary>
    public async Task<Startup> CreateStartup(StartupCreateInput createDto)
    {
        var startup = new StartupDbModel
        {
            CreatedAt = createDto.CreatedAt,
            FoundedDate = createDto.FoundedDate,
            Industry = createDto.Industry,
            Name = createDto.Name,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            startup.Id = createDto.Id;
        }
        if (createDto.FundingRounds != null)
        {
            startup.FundingRounds = await _context
                .FundingRounds.Where(fundingRound =>
                    createDto.FundingRounds.Select(t => t.Id).Contains(fundingRound.Id)
                )
                .ToListAsync();
        }

        _context.Startups.Add(startup);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<StartupDbModel>(startup.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Startup
    /// </summary>
    public async Task DeleteStartup(StartupWhereUniqueInput uniqueId)
    {
        var startup = await _context.Startups.FindAsync(uniqueId.Id);
        if (startup == null)
        {
            throw new NotFoundException();
        }

        _context.Startups.Remove(startup);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Startups
    /// </summary>
    public async Task<List<Startup>> Startups(StartupFindManyArgs findManyArgs)
    {
        var startups = await _context
            .Startups.Include(x => x.FundingRounds)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return startups.ConvertAll(startup => startup.ToDto());
    }

    /// <summary>
    /// Meta data about Startup records
    /// </summary>
    public async Task<MetadataDto> StartupsMeta(StartupFindManyArgs findManyArgs)
    {
        var count = await _context.Startups.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Startup
    /// </summary>
    public async Task<Startup> Startup(StartupWhereUniqueInput uniqueId)
    {
        var startups = await this.Startups(
            new StartupFindManyArgs { Where = new StartupWhereInput { Id = uniqueId.Id } }
        );
        var startup = startups.FirstOrDefault();
        if (startup == null)
        {
            throw new NotFoundException();
        }

        return startup;
    }

    /// <summary>
    /// Update one Startup
    /// </summary>
    public async Task UpdateStartup(StartupWhereUniqueInput uniqueId, StartupUpdateInput updateDto)
    {
        var startup = updateDto.ToModel(uniqueId);

        if (updateDto.FundingRounds != null)
        {
            startup.FundingRounds = await _context
                .FundingRounds.Where(fundingRound =>
                    updateDto.FundingRounds.Select(t => t).Contains(fundingRound.Id)
                )
                .ToListAsync();
        }

        _context.Entry(startup).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Startups.Any(e => e.Id == startup.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Connect multiple FundingRounds records to Startup
    /// </summary>
    public async Task ConnectFundingRounds(
        StartupWhereUniqueInput uniqueId,
        FundingRoundWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Startups.Include(x => x.FundingRounds)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .FundingRounds.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.FundingRounds);

        foreach (var child in childrenToConnect)
        {
            parent.FundingRounds.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple FundingRounds records from Startup
    /// </summary>
    public async Task DisconnectFundingRounds(
        StartupWhereUniqueInput uniqueId,
        FundingRoundWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Startups.Include(x => x.FundingRounds)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .FundingRounds.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.FundingRounds?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple FundingRounds records for Startup
    /// </summary>
    public async Task<List<FundingRound>> FindFundingRounds(
        StartupWhereUniqueInput uniqueId,
        FundingRoundFindManyArgs startupFindManyArgs
    )
    {
        var fundingRounds = await _context
            .FundingRounds.Where(m => m.StartupId == uniqueId.Id)
            .ApplyWhere(startupFindManyArgs.Where)
            .ApplySkip(startupFindManyArgs.Skip)
            .ApplyTake(startupFindManyArgs.Take)
            .ApplyOrderBy(startupFindManyArgs.SortBy)
            .ToListAsync();

        return fundingRounds.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple FundingRounds records for Startup
    /// </summary>
    public async Task UpdateFundingRounds(
        StartupWhereUniqueInput uniqueId,
        FundingRoundWhereUniqueInput[] childrenIds
    )
    {
        var startup = await _context
            .Startups.Include(t => t.FundingRounds)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (startup == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .FundingRounds.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        startup.FundingRounds = children;
        await _context.SaveChangesAsync();
    }
}
