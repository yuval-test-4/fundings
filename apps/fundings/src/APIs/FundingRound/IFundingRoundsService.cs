using Fundings.APIs.Common;
using Fundings.APIs.Dtos;

namespace Fundings.APIs;

public interface IFundingRoundsService
{
    /// <summary>
    /// Create one FundingRound
    /// </summary>
    public Task<FundingRound> CreateFundingRound(FundingRoundCreateInput fundinground);

    /// <summary>
    /// Delete one FundingRound
    /// </summary>
    public Task DeleteFundingRound(FundingRoundWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many FundingRounds
    /// </summary>
    public Task<List<FundingRound>> FundingRounds(FundingRoundFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about FundingRound records
    /// </summary>
    public Task<MetadataDto> FundingRoundsMeta(FundingRoundFindManyArgs findManyArgs);

    /// <summary>
    /// Get one FundingRound
    /// </summary>
    public Task<FundingRound> FundingRound(FundingRoundWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one FundingRound
    /// </summary>
    public Task UpdateFundingRound(
        FundingRoundWhereUniqueInput uniqueId,
        FundingRoundUpdateInput updateDto
    );

    /// <summary>
    /// Get a investor record for FundingRound
    /// </summary>
    public Task<Investor> GetInvestor(FundingRoundWhereUniqueInput uniqueId);

    /// <summary>
    /// Connect multiple Investors records to FundingRound
    /// </summary>
    public Task ConnectInvestors(
        FundingRoundWhereUniqueInput uniqueId,
        InvestorWhereUniqueInput[] investorsId
    );

    /// <summary>
    /// Disconnect multiple Investors records from FundingRound
    /// </summary>
    public Task DisconnectInvestors(
        FundingRoundWhereUniqueInput uniqueId,
        InvestorWhereUniqueInput[] investorsId
    );

    /// <summary>
    /// Find multiple Investors records for FundingRound
    /// </summary>
    public Task<List<Investor>> FindInvestors(
        FundingRoundWhereUniqueInput uniqueId,
        InvestorFindManyArgs InvestorFindManyArgs
    );

    /// <summary>
    /// Update multiple Investors records for FundingRound
    /// </summary>
    public Task UpdateInvestors(
        FundingRoundWhereUniqueInput uniqueId,
        InvestorWhereUniqueInput[] investorsId
    );

    /// <summary>
    /// Get a startup record for FundingRound
    /// </summary>
    public Task<Startup> GetStartup(FundingRoundWhereUniqueInput uniqueId);
}
