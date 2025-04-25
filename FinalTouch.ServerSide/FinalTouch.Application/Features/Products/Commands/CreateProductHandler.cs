using FinalTouch.Core.Entities;
using FinalTouch.Core.Interfaces;
using FluentValidation;
using MediatR;

namespace FinalTouch.Application.Features.Products.Commands;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, Product>
{
    private readonly IUnitOfWork _unit;
    private readonly IValidator<CreateProductCommand> _validator;

    public CreateProductHandler(IUnitOfWork unit, IValidator<CreateProductCommand> validator)
    {
        _unit = unit;
        _validator = validator;
    }

    public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            ImageUrl = request.ImageUrl,
            Type = request.Type,
            Brand = request.Brand,
            QuantityInStock = request.QuantityInStock
        };

        _unit.CommandRepository<Product>().Add(product);

        var result = await _unit.Complete();
        if (!result)
            throw new Exception("Failed to create product");

        return product;
    }
}
