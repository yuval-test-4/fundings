namespace Fundings.APIs.Dtos;

public class StartupWhereInput
{
    public DateTime? CreatedAt { get; set; }

    public DateTime? FoundedDate { get; set; }

    public List<string>? FundingRounds { get; set; }

    public string? Id { get; set; }

    public string? Industry { get; set; }

    public string? Name { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
