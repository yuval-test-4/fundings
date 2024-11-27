using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fundings.Infrastructure.Models;

[Table("Startups")]
public class StartupDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    public DateTime? FoundedDate { get; set; }

    public List<FundingRoundDbModel>? FundingRounds { get; set; } = new List<FundingRoundDbModel>();

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? Industry { get; set; }

    [StringLength(1000)]
    public string? Name { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
