using Fundings.APIs.Dtos;
using Fundings.Infrastructure.Models;

namespace Fundings.APIs.Extensions;

public static class StartupsExtensions
{
    public static Startup ToDto(this StartupDbModel model)
    {
        return new Startup
        {
            CreatedAt = model.CreatedAt,
            FoundedDate = model.FoundedDate,
            FundingRounds = model.FundingRounds?.Select(x => x.Id).ToList(),
            Id = model.Id,
            Industry = model.Industry,
            Name = model.Name,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static StartupDbModel ToModel(
        this StartupUpdateInput updateDto,
        StartupWhereUniqueInput uniqueId
    )
    {
        var startup = new StartupDbModel
        {
            Id = uniqueId.Id,
            FoundedDate = updateDto.FoundedDate,
            Industry = updateDto.Industry,
            Name = updateDto.Name
        };

        if (updateDto.CreatedAt != null)
        {
            startup.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            startup.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return startup;
    }
}
