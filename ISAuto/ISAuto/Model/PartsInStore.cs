using System;
using System.Collections.Generic;

namespace ISAuto.Model;

public partial class PartsInStore
{
    public int Id { get; set; }

    public int AutoPartId { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public virtual AutoPart AutoPart { get; set; } = null!;

    public virtual ICollection<PartsInOeder> PartsInOeders { get; set; } = new List<PartsInOeder>();
}
