using System;
using System.Collections.Generic;

namespace ISAuto.Model;

public partial class PartsInOeder
{
    public int Id { get; set; }

    public int PartsInStoreId { get; set; }

    public int OrderId { get; set; }

    public int Quantity { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual PartsInStore PartsInStore { get; set; } = null!;
}
