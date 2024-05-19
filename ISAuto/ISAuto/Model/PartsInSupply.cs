using System;
using System.Collections.Generic;

namespace ISAuto.Model;

public partial class PartsInSupply
{
    public int Id { get; set; }

    public int AutoPartId { get; set; }

    public int SupplyId { get; set; }

    public int Quantity { get; set; }

    public virtual AutoPart AutoPart { get; set; } = null!;

    public virtual Supply Supply { get; set; } = null!;
}
