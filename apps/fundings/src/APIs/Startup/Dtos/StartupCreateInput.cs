namespace Fundings.APIs.Dtos;

public class StartupCreateInput
{
    public DateTime CreatedAt { get; set; }

    public DateTime? FoundedDate { get; set; }

    public List<FundingRound>? FundingRounds { get; set; }

    public string? Id { get; set; }

    public string? Industry { get; set; }

    public string? Name { get; set; }

    public DateTime UpdatedAt { get; set; }
}
