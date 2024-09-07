using System;
using System.Collections.Generic;

namespace eGreetings.Models;

public partial class Report
{
    public int ReportId { get; set; }

    public DateTime ReportDate { get; set; }

    public int? TotalTransactions { get; set; }

    public decimal? TotalRevenue { get; set; }
}
