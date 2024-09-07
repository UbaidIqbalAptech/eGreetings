using System;
using System.Collections.Generic;

namespace eGreetings.Models;

public partial class Template
{
    public int TemplateId { get; set; }

    public string Category { get; set; } = null!;

    public string TemplateName { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<CardDesign> CardDesigns { get; set; } = new List<CardDesign>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
