using Fundings.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Fundings.Infrastructure;

public class FundingsDbContext : IdentityDbContext<IdentityUser>
{
    public FundingsDbContext(DbContextOptions<FundingsDbContext> options)
        : base(options) { }

    public DbSet<InvestorDbModel> Investors { get; set; }

    public DbSet<FundingRoundDbModel> FundingRounds { get; set; }

    public DbSet<StartupDbModel> Startups { get; set; }
}
