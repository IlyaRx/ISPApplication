using System;
using System.Collections.Generic;

namespace ISAuto.Model;

public partial class Supply
{
    public int Id { get; set; }

    public int SupplierId { get; set; }

    public int EmployeeId { get; set; }

    public decimal Cost { get; set; }

    public DateOnly SupplyDate { get; set; }

    public int TotalQuantity { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual ICollection<PartsInSupply> PartsInSupplies { get; set; } = new List<PartsInSupply>();

    public virtual Supplier Supplier { get; set; } = null!;
}
