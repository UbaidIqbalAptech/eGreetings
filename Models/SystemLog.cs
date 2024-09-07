using System;
using System.Collections.Generic;

namespace eGreetings.Models;

public partial class SystemLog
{
    public int LogId { get; set; }

    public DateTime? LogDate { get; set; }

    public string LogLevel { get; set; } = null!;

    public string Message { get; set; } = null!;

    public int? UserId { get; set; }

    public virtual User? User { get; set; }
}
