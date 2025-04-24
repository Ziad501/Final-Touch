using FinalTouch.Api.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalTouch.Application.Dtos.Validators
{
    public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderDtoValidator() 
        {
            RuleFor(x => x.CartId)
                .NotEmpty().WithMessage("Cart ID is required.");

            RuleFor(x => x.DeliveryMethodId)
                .GreaterThan(0).WithMessage("Delivery method must be selected.");

            RuleFor(x => x.ShippingAddress)
                .NotNull().WithMessage("Shipping address is required.");

            RuleFor(x => x.PaymentSummary)
                .NotNull().WithMessage("Payment summary is required.");
        }
    }
}
