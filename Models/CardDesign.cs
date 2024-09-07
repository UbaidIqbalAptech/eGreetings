using System;
using System.Collections.Generic;

namespace eGreetings.Models;

public partial class CardDesign
{
    public int DesignId { get; set; }

    public int? TemplateId { get; set; }

    public string DesignName { get; set; } = null!;

    public string? DesignImageUrl { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<DesignElement> DesignElements { get; set; } = new List<DesignElement>();

    public virtual Template? Template { get; set; }
}
