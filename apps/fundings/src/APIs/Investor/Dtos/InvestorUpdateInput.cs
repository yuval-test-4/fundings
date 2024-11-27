namespace Fundings.APIs.Dtos;

public class InvestorUpdateInput
{
    public DateTime? CreatedAt { get; set; }

    public string? Email { get; set; }

    public string? FundingRound { get; set; }

    public List<string>? FundingRounds { get; set; }

    public string? Id { get; set; }

    public string? Name { get; set; }

    public string? PhoneNumber { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
