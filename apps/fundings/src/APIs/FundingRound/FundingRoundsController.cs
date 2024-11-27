using Microsoft.AspNetCore.Mvc;

namespace Fundings.APIs;

[ApiController()]
public class FundingRoundsController : FundingRoundsControllerBase
{
    public FundingRoundsController(IFundingRoundsService service)
        : base(service) { }
}
