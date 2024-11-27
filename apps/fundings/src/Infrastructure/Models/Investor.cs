using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fundings.Infrastructure.Models;

[Table("Investors")]
public class InvestorDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    public string? Email { get; set; }

    public string? FundingRoundId { get; set; }

    [ForeignKey(nameof(FundingRoundId))]
    public FundingRoundDbModel? FundingRound { get; set; } = null;

    public List<FundingRoundDbModel>? FundingRounds { get; set; } = new List<FundingRoundDbModel>();

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? Name { get; set; }

    [StringLength(1000)]
    public string? PhoneNumber { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
