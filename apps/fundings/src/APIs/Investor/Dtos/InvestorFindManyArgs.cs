using Fundings.APIs.Common;
using Fundings.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fundings.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class InvestorFindManyArgs : FindManyInput<Investor, InvestorWhereInput> { }