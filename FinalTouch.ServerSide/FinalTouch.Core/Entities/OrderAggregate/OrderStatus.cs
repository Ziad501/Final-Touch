using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalTouch.Core.Entities.OrderAggregate
{
    public enum OrderStatus
    {
            Pending,
            PaymentReceived,
            PaymentFailed,
            PaymentMismatch,
            Refunded
    }
}
