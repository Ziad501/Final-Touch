using FinalTouch.Api.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalTouch.Application.Dtos.Validators
{
    public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator() 
        {

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500);

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0");

            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Picture URL is required.");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Product type is required.");

            RuleFor(x => x.Brand)
                .NotEmpty().WithMessage("Brand is required.");

            RuleFor(x => x.QuantityInStock)
                .GreaterThan(0).WithMessage("Quantity in stock must be at least 1");
        }
    }
}
