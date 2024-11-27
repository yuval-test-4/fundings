using Fundings.Infrastructure;

namespace Fundings.APIs;

public class StartupsService : StartupsServiceBase
{
    public StartupsService(FundingsDbContext context)
        : base(context) { }
}
