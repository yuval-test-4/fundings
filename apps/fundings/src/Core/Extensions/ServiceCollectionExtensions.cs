using Fundings.APIs;

namespace Fundings;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IFundingRoundsService, FundingRoundsService>();
        services.AddScoped<IInvestorsService, InvestorsService>();
        services.AddScoped<IStartupsService, StartupsService>();
    }
}
