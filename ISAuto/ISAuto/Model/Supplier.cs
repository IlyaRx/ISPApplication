using System;
using System.Collections.Generic;

namespace ISAuto.Model;

public partial class Supplier
{
    public int Id { get; set; }

    public int? ManufacturerId { get; set; }

    public string Name { get; set; } = null!;

    public virtual Manufacturer? Manufacturer { get; set; }

    public virtual ICollection<Supply> Supplies { get; set; } = new List<Supply>();
}
