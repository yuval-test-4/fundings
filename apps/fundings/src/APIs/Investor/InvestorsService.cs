using Fundings.Infrastructure;

namespace Fundings.APIs;

public class InvestorsService : InvestorsServiceBase
{
    public InvestorsService(FundingsDbContext context)
        : base(context) { }
}
