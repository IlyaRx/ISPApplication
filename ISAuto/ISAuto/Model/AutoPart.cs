using System;
using System.Collections.Generic;

namespace ISAuto.Model;

public partial class AutoPart
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int TypeId { get; set; }

    public decimal PurchasePrise { get; set; }

    public string? Image { get; set; }

    public int ManufacturerId { get; set; }

    public string? Description { get; set; }

    public virtual Manufacturer Manufacturer { get; set; } = null!;

    public virtual ICollection<PartsInStore> PartsInStores { get; set; } = new List<PartsInStore>();

    public virtual ICollection<PartsInSupply> PartsInSupplies { get; set; } = new List<PartsInSupply>();

    public virtual TypePart Type { get; set; } = null!;
}
