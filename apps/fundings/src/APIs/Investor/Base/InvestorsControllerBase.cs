using Fundings.APIs;
using Fundings.APIs.Common;
using Fundings.APIs.Dtos;
using Fundings.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fundings.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class InvestorsControllerBase : ControllerBase
{
    protected readonly IInvestorsService _service;

    public InvestorsControllerBase(IInvestorsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Investor
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Investor>> CreateInvestor(InvestorCreateInput input)
    {
        var investor = await _service.CreateInvestor(input);

        return CreatedAtAction(nameof(Investor), new { id = investor.Id }, investor);
    }

    /// <summary>
    /// Delete one Investor
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteInvestor([FromRoute()] InvestorWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteInvestor(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Investors
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Investor>>> Investors(
        [FromQuery()] InvestorFindManyArgs filter
    )
    {
        return Ok(await _service.Investors(filter));
    }

    /// <summary>
    /// Meta data about Investor records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> InvestorsMeta(
        [FromQuery()] InvestorFindManyArgs filter
    )
    {
        return Ok(await _service.InvestorsMeta(filter));
    }

    /// <summary>
    /// Get one Investor
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Investor>> Investor(
        [FromRoute()] InvestorWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Investor(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Investor
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateInvestor(
        [FromRoute()] InvestorWhereUniqueInput uniqueId,
        [FromQuery()] InvestorUpdateInput investorUpdateDto
    )
    {
        try
        {
            await _service.UpdateInvestor(uniqueId, investorUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a fundingRound record for Investor
    /// </summary>
    [HttpGet("{Id}/fundingRound")]
    public async Task<ActionResult<List<FundingRound>>> GetFundingRound(
        [FromRoute()] InvestorWhereUniqueInput uniqueId
    )
    {
        var fundingRound = await _service.GetFundingRound(uniqueId);
        return Ok(fundingRound);
    }

    /// <summary>
    /// Connect multiple FundingRounds records to Investor
    /// </summary>
    [HttpPost("{Id}/fundingRounds")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> ConnectFundingRounds(
        [FromRoute()] InvestorWhereUniqueInput uniqueId,
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
    /// Disconnect multiple FundingRounds records from Investor
    /// </summary>
    [HttpDelete("{Id}/fundingRounds")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DisconnectFundingRounds(
        [FromRoute()] InvestorWhereUniqueInput uniqueId,
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
    /// Find multiple FundingRounds records for Investor
    /// </summary>
    [HttpGet("{Id}/fundingRounds")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<FundingRound>>> FindFundingRounds(
        [FromRoute()] InvestorWhereUniqueInput uniqueId,
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
    /// Update multiple FundingRounds records for Investor
    /// </summary>
    [HttpPatch("{Id}/fundingRounds")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateFundingRounds(
        [FromRoute()] InvestorWhereUniqueInput uniqueId,
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
