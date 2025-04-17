using FinalTouch.Core.Entities;
using FinalTouch.Core.Interfaces;
using FinalTouch.InfraStructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalTouch.Api.Controllers
{
    public class PaymentController(IPaymentService paymentService,IGenericRepository<DeliveryMethod> dmRepo) :BaseApiController
    {
        [Authorize]
        [HttpPost("{cartId}")]
        public async Task<ActionResult<ShoppingCart>> CreateOrUpdatePaymentIntent(string cartId)
        {
            var cart = await paymentService.CreateOrUpdatePaymentIntent(cartId);

            if (cart == null) return BadRequest("Problem with your cart");

            return Ok(cart);
        }

        [HttpGet("delivery-methods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            return Ok(await dmRepo.GetAllAsync());
        }
    
    }
}
