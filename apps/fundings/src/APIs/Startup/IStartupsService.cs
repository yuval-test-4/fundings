using Fundings.APIs.Common;
using Fundings.APIs.Dtos;

namespace Fundings.APIs;

public interface IStartupsService
{
    /// <summary>
    /// Create one Startup
    /// </summary>
    public Task<Startup> CreateStartup(StartupCreateInput startup);

    /// <summary>
    /// Delete one Startup
    /// </summary>
    public Task DeleteStartup(StartupWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Startups
    /// </summary>
    public Task<List<Startup>> Startups(StartupFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Startup records
    /// </summary>
    public Task<MetadataDto> StartupsMeta(StartupFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Startup
    /// </summary>
    public Task<Startup> Startup(StartupWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Startup
    /// </summary>
    public Task UpdateStartup(StartupWhereUniqueInput uniqueId, StartupUpdateInput updateDto);

    /// <summary>
    /// Connect multiple FundingRounds records to Startup
    /// </summary>
    public Task ConnectFundingRounds(
        StartupWhereUniqueInput uniqueId,
        FundingRoundWhereUniqueInput[] fundingRoundsId
    );

    /// <summary>
    /// Disconnect multiple FundingRounds records from Startup
    /// </summary>
    public Task DisconnectFundingRounds(
        StartupWhereUniqueInput uniqueId,
        FundingRoundWhereUniqueInput[] fundingRoundsId
    );

    /// <summary>
    /// Find multiple FundingRounds records for Startup
    /// </summary>
    public Task<List<FundingRound>> FindFundingRounds(
        StartupWhereUniqueInput uniqueId,
        FundingRoundFindManyArgs FundingRoundFindManyArgs
    );

    /// <summary>
    /// Update multiple FundingRounds records for Startup
    /// </summary>
    public Task UpdateFundingRounds(
        StartupWhereUniqueInput uniqueId,
        FundingRoundWhereUniqueInput[] fundingRoundsId
    );
}
