using FluentValidation;

namespace FinalTouch.Application.Features.Products.Commands;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Product ID is required.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500);

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0");

        RuleFor(x => x.ImageUrl)
            .NotEmpty().WithMessage("Image URL is required.");

        RuleFor(x => x.Type)
            .NotEmpty().WithMessage("Product type is required.");

        RuleFor(x => x.Brand)
            .NotEmpty().WithMessage("Brand is required.");

        RuleFor(x => x.QuantityInStock)
            .GreaterThan(0).WithMessage("Quantity in stock must be at least 1");
    }
}
