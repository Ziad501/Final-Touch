
using FinalTouch.Core.Entities;

namespace FinalTouch.Core.Interfaces
{

    public interface IPaymentService
    {
        Task<ShoppingCart?> CreateOrUpdatePaymentIntent(string cartId);
        Task<string> RefundPayment(string paymentIntentId);
	}
}
