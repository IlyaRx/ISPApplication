using System;
using System.Collections.Generic;

namespace ISAuto.Model;

public partial class Manufacturer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<AutoPart> AutoParts { get; set; } = new List<AutoPart>();

    public virtual ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();
}
