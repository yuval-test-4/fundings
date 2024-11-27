using Fundings.APIs.Dtos;
using Fundings.Infrastructure.Models;

namespace Fundings.APIs.Extensions;

public static class InvestorsExtensions
{
    public static Investor ToDto(this InvestorDbModel model)
    {
        return new Investor
        {
            CreatedAt = model.CreatedAt,
            Email = model.Email,
            FundingRound = model.FundingRoundId,
            FundingRounds = model.FundingRounds?.Select(x => x.Id).ToList(),
            Id = model.Id,
            Name = model.Name,
            PhoneNumber = model.PhoneNumber,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static InvestorDbModel ToModel(
        this InvestorUpdateInput updateDto,
        InvestorWhereUniqueInput uniqueId
    )
    {
        var investor = new InvestorDbModel
        {
            Id = uniqueId.Id,
            Email = updateDto.Email,
            Name = updateDto.Name,
            PhoneNumber = updateDto.PhoneNumber
        };

        if (updateDto.CreatedAt != null)
        {
            investor.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.FundingRound != null)
        {
            investor.FundingRoundId = updateDto.FundingRound;
        }
        if (updateDto.UpdatedAt != null)
        {
            investor.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return investor;
    }
}
