using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fundings.Infrastructure.Models;

[Table("FundingRounds")]
public class FundingRoundDbModel
{
    [Range(-999999999, 999999999)]
    public double? Amount { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    public DateTime? Date { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public string? InvestorId { get; set; }

    [ForeignKey(nameof(InvestorId))]
    public InvestorDbModel? Investor { get; set; } = null;

    public List<InvestorDbModel>? Investors { get; set; } = new List<InvestorDbModel>();

    [StringLength(1000)]
    public string? RoundName { get; set; }

    public string? StartupId { get; set; }

    [ForeignKey(nameof(StartupId))]
    public StartupDbModel? Startup { get; set; } = null;

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
