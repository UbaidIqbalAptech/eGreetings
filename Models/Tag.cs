using System;
using System.Collections.Generic;

namespace eGreetings.Models;

public partial class Tag
{
    public int TagId { get; set; }

    public string TagName { get; set; } = null!;

    public virtual ICollection<Template> Templates { get; set; } = new List<Template>();
}
