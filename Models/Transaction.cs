using System;
using System.Collections.Generic;

namespace eGreetings.Models;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public int? UserId { get; set; }

    public int? TemplateId { get; set; }

    public string RecipientEmail { get; set; } = null!;

    public DateTime? SendDate { get; set; }

    public virtual Template? Template { get; set; }

    public virtual User? User { get; set; }
}
