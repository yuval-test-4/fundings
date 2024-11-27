using Fundings.APIs;
using Fundings.APIs.Common;
using Fundings.APIs.Dtos;
using Fundings.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fundings.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class StartupsControllerBase : ControllerBase
{
    protected readonly IStartupsService _service;

    public StartupsControllerBase(IStartupsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Startup
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Startup>> CreateStartup(StartupCreateInput input)
    {
        var startup = await _service.CreateStartup(input);

        return CreatedAtAction(nameof(Startup), new { id = startup.Id }, startup);
    }

    /// <summary>
    /// Delete one Startup
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteStartup([FromRoute()] StartupWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteStartup(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Startups
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Startup>>> Startups(
        [FromQuery()] StartupFindManyArgs filter
    )
    {
        return Ok(await _service.Startups(filter));
    }

    /// <summary>
    /// Meta data about Startup records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> StartupsMeta(
        [FromQuery()] StartupFindManyArgs filter
    )
    {
        return Ok(await _service.StartupsMeta(filter));
    }

    /// <summary>
    /// Get one Startup
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Startup>> Startup([FromRoute()] StartupWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Startup(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Startup
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateStartup(
        [FromRoute()] StartupWhereUniqueInput uniqueId,
        [FromQuery()] StartupUpdateInput startupUpdateDto
    )
    {
        try
        {
            await _service.UpdateStartup(uniqueId, startupUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple FundingRounds records to Startup
    /// </summary>
    [HttpPost("{Id}/fundingRounds")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> ConnectFundingRounds(
        [FromRoute()] StartupWhereUniqueInput uniqueId,
        [FromQuery()] FundingRoundWhereUniqueInput[] fundingRoundsId
    )
    {
        try
        {
            await _service.ConnectFundingRounds(uniqueId, fundingRoundsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple FundingRounds records from Startup
    /// </summary>
    [HttpDelete("{Id}/fundingRounds")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DisconnectFundingRounds(
        [FromRoute()] StartupWhereUniqueInput uniqueId,
        [FromBody()] FundingRoundWhereUniqueInput[] fundingRoundsId
    )
    {
        try
        {
            await _service.DisconnectFundingRounds(uniqueId, fundingRoundsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple FundingRounds records for Startup
    /// </summary>
    [HttpGet("{Id}/fundingRounds")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<FundingRound>>> FindFundingRounds(
        [FromRoute()] StartupWhereUniqueInput uniqueId,
        [FromQuery()] FundingRoundFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindFundingRounds(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple FundingRounds records for Startup
    /// </summary>
    [HttpPatch("{Id}/fundingRounds")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateFundingRounds(
        [FromRoute()] StartupWhereUniqueInput uniqueId,
        [FromBody()] FundingRoundWhereUniqueInput[] fundingRoundsId
    )
    {
        try
        {
            await _service.UpdateFundingRounds(uniqueId, fundingRoundsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
