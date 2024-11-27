using Fundings.APIs;
using Fundings.APIs.Common;
using Fundings.APIs.Dtos;
using Fundings.APIs.Errors;
using Fundings.APIs.Extensions;
using Fundings.Infrastructure;
using Fundings.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Fundings.APIs;

public abstract class InvestorsServiceBase : IInvestorsService
{
    protected readonly FundingsDbContext _context;

    public InvestorsServiceBase(FundingsDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Investor
    /// </summary>
    public async Task<Investor> CreateInvestor(InvestorCreateInput createDto)
    {
        var investor = new InvestorDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Email = createDto.Email,
            Name = createDto.Name,
            PhoneNumber = createDto.PhoneNumber,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            investor.Id = createDto.Id;
        }
        if (createDto.FundingRound != null)
        {
            investor.FundingRound = await _context
                .FundingRounds.Where(fundingRound => createDto.FundingRound.Id == fundingRound.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.FundingRounds != null)
        {
            investor.FundingRounds = await _context
                .FundingRounds.Where(fundingRound =>
                    createDto.FundingRounds.Select(t => t.Id).Contains(fundingRound.Id)
                )
                .ToListAsync();
        }

        _context.Investors.Add(investor);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<InvestorDbModel>(investor.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Investor
    /// </summary>
    public async Task DeleteInvestor(InvestorWhereUniqueInput uniqueId)
    {
        var investor = await _context.Investors.FindAsync(uniqueId.Id);
        if (investor == null)
        {
            throw new NotFoundException();
        }

        _context.Investors.Remove(investor);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Investors
    /// </summary>
    public async Task<List<Investor>> Investors(InvestorFindManyArgs findManyArgs)
    {
        var investors = await _context
            .Investors.Include(x => x.FundingRound)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return investors.ConvertAll(investor => investor.ToDto());
    }

    /// <summary>
    /// Meta data about Investor records
    /// </summary>
    public async Task<MetadataDto> InvestorsMeta(InvestorFindManyArgs findManyArgs)
    {
        var count = await _context.Investors.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Investor
    /// </summary>
    public async Task<Investor> Investor(InvestorWhereUniqueInput uniqueId)
    {
        var investors = await this.Investors(
            new InvestorFindManyArgs { Where = new InvestorWhereInput { Id = uniqueId.Id } }
        );
        var investor = investors.FirstOrDefault();
        if (investor == null)
        {
            throw new NotFoundException();
        }

        return investor;
    }

    /// <summary>
    /// Update one Investor
    /// </summary>
    public async Task UpdateInvestor(
        InvestorWhereUniqueInput uniqueId,
        InvestorUpdateInput updateDto
    )
    {
        var investor = updateDto.ToModel(uniqueId);

        if (updateDto.FundingRound != null)
        {
            investor.FundingRound = await _context
                .FundingRounds.Where(fundingRound => updateDto.FundingRound == fundingRound.Id)
                .FirstOrDefaultAsync();
        }

        if (updateDto.FundingRounds != null)
        {
            investor.FundingRounds = await _context
                .FundingRounds.Where(fundingRound =>
                    updateDto.FundingRounds.Select(t => t).Contains(fundingRound.Id)
                )
                .ToListAsync();
        }

        _context.Entry(investor).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Investors.Any(e => e.Id == investor.Id))
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
    /// Get a fundingRound record for Investor
    /// </summary>
    public async Task<FundingRound> GetFundingRound(InvestorWhereUniqueInput uniqueId)
    {
        var investor = await _context
            .Investors.Where(investor => investor.Id == uniqueId.Id)
            .Include(investor => investor.FundingRound)
            .FirstOrDefaultAsync();
        if (investor == null)
        {
            throw new NotFoundException();
        }
        return investor.FundingRound.ToDto();
    }

    /// <summary>
    /// Connect multiple FundingRounds records to Investor
    /// </summary>
    public async Task ConnectFundingRounds(
        InvestorWhereUniqueInput uniqueId,
        FundingRoundWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Investors.Include(x => x.FundingRounds)
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
    /// Disconnect multiple FundingRounds records from Investor
    /// </summary>
    public async Task DisconnectFundingRounds(
        InvestorWhereUniqueInput uniqueId,
        FundingRoundWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Investors.Include(x => x.FundingRounds)
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
    /// Find multiple FundingRounds records for Investor
    /// </summary>
    public async Task<List<FundingRound>> FindFundingRounds(
        InvestorWhereUniqueInput uniqueId,
        FundingRoundFindManyArgs investorFindManyArgs
    )
    {
        var fundingRounds = await _context
            .FundingRounds.Where(m => m.InvestorId == uniqueId.Id)
            .ApplyWhere(investorFindManyArgs.Where)
            .ApplySkip(investorFindManyArgs.Skip)
            .ApplyTake(investorFindManyArgs.Take)
            .ApplyOrderBy(investorFindManyArgs.SortBy)
            .ToListAsync();

        return fundingRounds.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple FundingRounds records for Investor
    /// </summary>
    public async Task UpdateFundingRounds(
        InvestorWhereUniqueInput uniqueId,
        FundingRoundWhereUniqueInput[] childrenIds
    )
    {
        var investor = await _context
            .Investors.Include(t => t.FundingRounds)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (investor == null)
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

        investor.FundingRounds = children;
        await _context.SaveChangesAsync();
    }
}
