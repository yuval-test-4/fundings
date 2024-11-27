namespace Fundings.APIs.Dtos;

public class FundingRoundCreateInput
{
    public double? Amount { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? Date { get; set; }

    public string? Id { get; set; }

    public Investor? Investor { get; set; }

    public List<Investor>? Investors { get; set; }

    public string? RoundName { get; set; }

    public Startup? Startup { get; set; }

    public DateTime UpdatedAt { get; set; }
}
