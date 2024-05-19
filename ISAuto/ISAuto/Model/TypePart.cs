using System;
using System.Collections.Generic;

namespace ISAuto.Model;

public partial class TypePart
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Imade { get; set; }

    public virtual ICollection<AutoPart> AutoParts { get; set; } = new List<AutoPart>();
}
