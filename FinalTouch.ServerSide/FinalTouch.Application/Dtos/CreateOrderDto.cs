using FinalTouch.Core.Entities.OrderAggregate;

namespace FinalTouch.Api.Dtos
{
    public class CreateOrderDto
    {
        public string CartId { get; set; } = string.Empty;
        public int DeliveryMethodId { get; set; }
        public ShippingAddress ShippingAddress { get; set; } = null!;
        public PaymentSummary PaymentSummary { get; set; } = null!;
        public decimal Discount { get; set; }
    }
}
