﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalTouch.Core.Entities.OrderAggregate
{
    public class OrderItem : BaseEntity
    {
            public ProductItemOrdered ItemOrdered { get; set; } = null!;
            public decimal Price { get; set; }
            public int Quantity { get; set; }
        }
}
