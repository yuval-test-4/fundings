namespace Fundings.APIs.Dtos;

public class InvestorCreateInput
{
    public DateTime CreatedAt { get; set; }

    public string? Email { get; set; }

    public FundingRound? FundingRound { get; set; }

    public List<FundingRound>? FundingRounds { get; set; }

    public string? Id { get; set; }

    public string? Name { get; set; }

    public string? PhoneNumber { get; set; }

    public DateTime UpdatedAt { get; set; }
}
