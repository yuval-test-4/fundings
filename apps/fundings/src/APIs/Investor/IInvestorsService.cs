using Fundings.APIs.Common;
using Fundings.APIs.Dtos;

namespace Fundings.APIs;

public interface IInvestorsService
{
    /// <summary>
    /// Create one Investor
    /// </summary>
    public Task<Investor> CreateInvestor(InvestorCreateInput investor);

    /// <summary>
    /// Delete one Investor
    /// </summary>
    public Task DeleteInvestor(InvestorWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Investors
    /// </summary>
    public Task<List<Investor>> Investors(InvestorFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Investor records
    /// </summary>
    public Task<MetadataDto> InvestorsMeta(InvestorFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Investor
    /// </summary>
    public Task<Investor> Investor(InvestorWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Investor
    /// </summary>
    public Task UpdateInvestor(InvestorWhereUniqueInput uniqueId, InvestorUpdateInput updateDto);

    /// <summary>
    /// Get a fundingRound record for Investor
    /// </summary>
    public Task<FundingRound> GetFundingRound(InvestorWhereUniqueInput uniqueId);

    /// <summary>
    /// Connect multiple FundingRounds records to Investor
    /// </summary>
    public Task ConnectFundingRounds(
        InvestorWhereUniqueInput uniqueId,
        FundingRoundWhereUniqueInput[] fundingRoundsId
    );

    /// <summary>
    /// Disconnect multiple FundingRounds records from Investor
    /// </summary>
    public Task DisconnectFundingRounds(
        InvestorWhereUniqueInput uniqueId,
        FundingRoundWhereUniqueInput[] fundingRoundsId
    );

    /// <summary>
    /// Find multiple FundingRounds records for Investor
    /// </summary>
    public Task<List<FundingRound>> FindFundingRounds(
        InvestorWhereUniqueInput uniqueId,
        FundingRoundFindManyArgs FundingRoundFindManyArgs
    );

    /// <summary>
    /// Update multiple FundingRounds records for Investor
    /// </summary>
    public Task UpdateFundingRounds(
        InvestorWhereUniqueInput uniqueId,
        FundingRoundWhereUniqueInput[] fundingRoundsId
    );
}
