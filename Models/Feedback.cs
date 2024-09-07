using System;
using System.Collections.Generic;

namespace eGreetings.Models;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public int? UserId { get; set; }

    public DateTime? FeedbackDate { get; set; }

    public string Message { get; set; } = null!;

    public virtual User? User { get; set; }
}
