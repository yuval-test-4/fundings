using Fundings.APIs;
using Fundings.APIs.Common;
using Fundings.APIs.Dtos;
using Fundings.APIs.Errors;
using Fundings.APIs.Extensions;
using Fundings.Infrastructure;
using Fundings.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Fundings.APIs;

public abstract class FundingRoundsServiceBase : IFundingRoundsService
{
    protected readonly FundingsDbContext _context;

    public FundingRoundsServiceBase(FundingsDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one FundingRound
    /// </summary>
    public async Task<FundingRound> CreateFundingRound(FundingRoundCreateInput createDto)
    {
        var fundingRound = new FundingRoundDbModel
        {
            Amount = createDto.Amount,
            CreatedAt = createDto.CreatedAt,
            Date = createDto.Date,
            RoundName = createDto.RoundName,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            fundingRound.Id = createDto.Id;
        }
        if (createDto.Investor != null)
        {
            fundingRound.Investor = await _context
                .Investors.Where(investor => createDto.Investor.Id == investor.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.Investors != null)
        {
            fundingRound.Investors = await _context
                .Investors.Where(investor =>
                    createDto.Investors.Select(t => t.Id).Contains(investor.Id)
                )
                .ToListAsync();
        }

        if (createDto.Startup != null)
        {
            fundingRound.Startup = await _context
                .Startups.Where(startup => createDto.Startup.Id == startup.Id)
                .FirstOrDefaultAsync();
        }

        _context.FundingRounds.Add(fundingRound);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<FundingRoundDbModel>(fundingRound.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one FundingRound
    /// </summary>
    public async Task DeleteFundingRound(FundingRoundWhereUniqueInput uniqueId)
    {
        var fundingRound = await _context.FundingRounds.FindAsync(uniqueId.Id);
        if (fundingRound == null)
        {
            throw new NotFoundException();
        }

        _context.FundingRounds.Remove(fundingRound);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many FundingRounds
    /// </summary>
    public async Task<List<FundingRound>> FundingRounds(FundingRoundFindManyArgs findManyArgs)
    {
        var fundingRounds = await _context
            .FundingRounds.Include(x => x.Investor)
            .Include(x => x.Startup)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return fundingRounds.ConvertAll(fundingRound => fundingRound.ToDto());
    }

    /// <summary>
    /// Meta data about FundingRound records
    /// </summary>
    public async Task<MetadataDto> FundingRoundsMeta(FundingRoundFindManyArgs findManyArgs)
    {
        var count = await _context.FundingRounds.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one FundingRound
    /// </summary>
    public async Task<FundingRound> FundingRound(FundingRoundWhereUniqueInput uniqueId)
    {
        var fundingRounds = await this.FundingRounds(
            new FundingRoundFindManyArgs { Where = new FundingRoundWhereInput { Id = uniqueId.Id } }
        );
        var fundingRound = fundingRounds.FirstOrDefault();
        if (fundingRound == null)
        {
            throw new NotFoundException();
        }

        return fundingRound;
    }

    /// <summary>
    /// Update one FundingRound
    /// </summary>
    public async Task UpdateFundingRound(
        FundingRoundWhereUniqueInput uniqueId,
        FundingRoundUpdateInput updateDto
    )
    {
        var fundingRound = updateDto.ToModel(uniqueId);

        if (updateDto.Investor != null)
        {
            fundingRound.Investor = await _context
                .Investors.Where(investor => updateDto.Investor == investor.Id)
                .FirstOrDefaultAsync();
        }

        if (updateDto.Investors != null)
        {
            fundingRound.Investors = await _context
                .Investors.Where(investor =>
                    updateDto.Investors.Select(t => t).Contains(investor.Id)
                )
                .ToListAsync();
        }

        if (updateDto.Startup != null)
        {
            fundingRound.Startup = await _context
                .Startups.Where(startup => updateDto.Startup == startup.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(fundingRound).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.FundingRounds.Any(e => e.Id == fundingRound.Id))
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
    /// Get a investor record for FundingRound
    /// </summary>
    public async Task<Investor> GetInvestor(FundingRoundWhereUniqueInput uniqueId)
    {
        var fundingRound = await _context
            .FundingRounds.Where(fundingRound => fundingRound.Id == uniqueId.Id)
            .Include(fundingRound => fundingRound.Investor)
            .FirstOrDefaultAsync();
        if (fundingRound == null)
        {
            throw new NotFoundException();
        }
        return fundingRound.Investor.ToDto();
    }

    /// <summary>
    /// Connect multiple Investors records to FundingRound
    /// </summary>
    public async Task ConnectInvestors(
        FundingRoundWhereUniqueInput uniqueId,
        InvestorWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .FundingRounds.Include(x => x.Investors)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Investors.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Investors);

        foreach (var child in childrenToConnect)
        {
            parent.Investors.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Investors records from FundingRound
    /// </summary>
    public async Task DisconnectInvestors(
        FundingRoundWhereUniqueInput uniqueId,
        InvestorWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .FundingRounds.Include(x => x.Investors)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Investors.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Investors?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Investors records for FundingRound
    /// </summary>
    public async Task<List<Investor>> FindInvestors(
        FundingRoundWhereUniqueInput uniqueId,
        InvestorFindManyArgs fundingRoundFindManyArgs
    )
    {
        var investors = await _context
            .Investors.Where(m => m.FundingRoundId == uniqueId.Id)
            .ApplyWhere(fundingRoundFindManyArgs.Where)
            .ApplySkip(fundingRoundFindManyArgs.Skip)
            .ApplyTake(fundingRoundFindManyArgs.Take)
            .ApplyOrderBy(fundingRoundFindManyArgs.SortBy)
            .ToListAsync();

        return investors.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Investors records for FundingRound
    /// </summary>
    public async Task UpdateInvestors(
        FundingRoundWhereUniqueInput uniqueId,
        InvestorWhereUniqueInput[] childrenIds
    )
    {
        var fundingRound = await _context
            .FundingRounds.Include(t => t.Investors)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (fundingRound == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Investors.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        fundingRound.Investors = children;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Get a startup record for FundingRound
    /// </summary>
    public async Task<Startup> GetStartup(FundingRoundWhereUniqueInput uniqueId)
    {
        var fundingRound = await _context
            .FundingRounds.Where(fundingRound => fundingRound.Id == uniqueId.Id)
            .Include(fundingRound => fundingRound.Startup)
            .FirstOrDefaultAsync();
        if (fundingRound == null)
        {
            throw new NotFoundException();
        }
        return fundingRound.Startup.ToDto();
    }
}
