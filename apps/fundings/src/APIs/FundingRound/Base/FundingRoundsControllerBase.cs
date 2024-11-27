using Fundings.APIs;
using Fundings.APIs.Common;
using Fundings.APIs.Dtos;
using Fundings.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fundings.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class FundingRoundsControllerBase : ControllerBase
{
    protected readonly IFundingRoundsService _service;

    public FundingRoundsControllerBase(IFundingRoundsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one FundingRound
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<FundingRound>> CreateFundingRound(FundingRoundCreateInput input)
    {
        var fundingRound = await _service.CreateFundingRound(input);

        return CreatedAtAction(nameof(FundingRound), new { id = fundingRound.Id }, fundingRound);
    }

    /// <summary>
    /// Delete one FundingRound
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteFundingRound(
        [FromRoute()] FundingRoundWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteFundingRound(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many FundingRounds
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<FundingRound>>> FundingRounds(
        [FromQuery()] FundingRoundFindManyArgs filter
    )
    {
        return Ok(await _service.FundingRounds(filter));
    }

    /// <summary>
    /// Meta data about FundingRound records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> FundingRoundsMeta(
        [FromQuery()] FundingRoundFindManyArgs filter
    )
    {
        return Ok(await _service.FundingRoundsMeta(filter));
    }

    /// <summary>
    /// Get one FundingRound
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<FundingRound>> FundingRound(
        [FromRoute()] FundingRoundWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.FundingRound(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one FundingRound
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateFundingRound(
        [FromRoute()] FundingRoundWhereUniqueInput uniqueId,
        [FromQuery()] FundingRoundUpdateInput fundingRoundUpdateDto
    )
    {
        try
        {
            await _service.UpdateFundingRound(uniqueId, fundingRoundUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a investor record for FundingRound
    /// </summary>
    [HttpGet("{Id}/investor")]
    public async Task<ActionResult<List<Investor>>> GetInvestor(
        [FromRoute()] FundingRoundWhereUniqueInput uniqueId
    )
    {
        var investor = await _service.GetInvestor(uniqueId);
        return Ok(investor);
    }

    /// <summary>
    /// Connect multiple Investors records to FundingRound
    /// </summary>
    [HttpPost("{Id}/investors")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> ConnectInvestors(
        [FromRoute()] FundingRoundWhereUniqueInput uniqueId,
        [FromQuery()] InvestorWhereUniqueInput[] investorsId
    )
    {
        try
        {
            await _service.ConnectInvestors(uniqueId, investorsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Investors records from FundingRound
    /// </summary>
    [HttpDelete("{Id}/investors")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DisconnectInvestors(
        [FromRoute()] FundingRoundWhereUniqueInput uniqueId,
        [FromBody()] InvestorWhereUniqueInput[] investorsId
    )
    {
        try
        {
            await _service.DisconnectInvestors(uniqueId, investorsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Investors records for FundingRound
    /// </summary>
    [HttpGet("{Id}/investors")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Investor>>> FindInvestors(
        [FromRoute()] FundingRoundWhereUniqueInput uniqueId,
        [FromQuery()] InvestorFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindInvestors(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Investors records for FundingRound
    /// </summary>
    [HttpPatch("{Id}/investors")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateInvestors(
        [FromRoute()] FundingRoundWhereUniqueInput uniqueId,
        [FromBody()] InvestorWhereUniqueInput[] investorsId
    )
    {
        try
        {
            await _service.UpdateInvestors(uniqueId, investorsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a startup record for FundingRound
    /// </summary>
    [HttpGet("{Id}/startup")]
    public async Task<ActionResult<List<Startup>>> GetStartup(
        [FromRoute()] FundingRoundWhereUniqueInput uniqueId
    )
    {
        var startup = await _service.GetStartup(uniqueId);
        return Ok(startup);
    }
}
