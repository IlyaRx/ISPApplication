using System;
using System.Collections.Generic;

namespace ISAuto.Model;

public partial class Order
{
    public int Id { get; set; }

    public decimal Cost { get; set; }

    public DateTime DateTimePurchase { get; set; }

    public virtual ICollection<PartsInOeder> PartsInOeders { get; set; } = new List<PartsInOeder>();
}
