using Microsoft.AspNetCore.Mvc;

namespace Fundings.APIs;

[ApiController()]
public class StartupsController : StartupsControllerBase
{
    public StartupsController(IStartupsService service)
        : base(service) { }
}
