using Microsoft.AspNetCore.Mvc;

namespace Fundings.APIs;

[ApiController()]
public class InvestorsController : InvestorsControllerBase
{
    public InvestorsController(IInvestorsService service)
        : base(service) { }
}
