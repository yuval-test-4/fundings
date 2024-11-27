using Fundings.Infrastructure;

namespace Fundings.APIs;

public class FundingRoundsService : FundingRoundsServiceBase
{
    public FundingRoundsService(FundingsDbContext context)
        : base(context) { }
}
