using Fundings.APIs.Dtos;
using Fundings.Infrastructure.Models;

namespace Fundings.APIs.Extensions;

public static class FundingRoundsExtensions
{
    public static FundingRound ToDto(this FundingRoundDbModel model)
    {
        return new FundingRound
        {
            Amount = model.Amount,
            CreatedAt = model.CreatedAt,
            Date = model.Date,
            Id = model.Id,
            Investor = model.InvestorId,
            Investors = model.Investors?.Select(x => x.Id).ToList(),
            RoundName = model.RoundName,
            Startup = model.StartupId,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static FundingRoundDbModel ToModel(
        this FundingRoundUpdateInput updateDto,
        FundingRoundWhereUniqueInput uniqueId
    )
    {
        var fundingRound = new FundingRoundDbModel
        {
            Id = uniqueId.Id,
            Amount = updateDto.Amount,
            Date = updateDto.Date,
            RoundName = updateDto.RoundName
        };

        if (updateDto.CreatedAt != null)
        {
            fundingRound.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Investor != null)
        {
            fundingRound.InvestorId = updateDto.Investor;
        }
        if (updateDto.Startup != null)
        {
            fundingRound.StartupId = updateDto.Startup;
        }
        if (updateDto.UpdatedAt != null)
        {
            fundingRound.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return fundingRound;
    }
}
