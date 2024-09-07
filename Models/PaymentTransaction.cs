using System;
using System.Collections.Generic;

namespace eGreetings.Models;

public partial class PaymentTransaction
{
    public int PaymentId { get; set; }

    public int? SubscriptionId { get; set; }

    public decimal Amount { get; set; }

    public DateTime? PaymentDate { get; set; }

    public string? PaymentStatus { get; set; }

    public string? TransactionId { get; set; }

    public virtual Subscription? Subscription { get; set; }
}
