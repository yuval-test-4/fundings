namespace Fundings.APIs.Dtos;

public class FundingRoundWhereInput
{
    public double? Amount { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? Date { get; set; }

    public string? Id { get; set; }

    public string? Investor { get; set; }

    public List<string>? Investors { get; set; }

    public string? RoundName { get; set; }

    public string? Startup { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
