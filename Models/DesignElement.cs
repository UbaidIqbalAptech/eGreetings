using System;
using System.Collections.Generic;

namespace eGreetings.Models;

public partial class DesignElement
{
    public int ElementId { get; set; }

    public int? DesignId { get; set; }

    public string ElementType { get; set; } = null!;

    public string? ContentUrl { get; set; }

    public string? TextContent { get; set; }

    public int PositionX { get; set; }

    public int PositionY { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }

    public virtual CardDesign? Design { get; set; }
}
