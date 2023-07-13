﻿using System;
using System.Collections.Generic;

namespace MSIT147.Estore.DataLayer.EFModels;

public partial class OrderList
{
    public int OrderListId { get; set; }

    public int OrderId { get; set; }

    public int SpecId { get; set; }

    public int Quantity { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Spec Spec { get; set; } = null!;
}
