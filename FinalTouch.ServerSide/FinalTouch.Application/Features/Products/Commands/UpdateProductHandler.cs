using FinalTouch.Core.Entities;
using FinalTouch.Core.Interfaces;
using FluentValidation;
using MediatR;

namespace FinalTouch.Application.Features.Products.Commands;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, bool>
{
    private readonly IUnitOfWork _unit;
    private readonly IValidator<UpdateProductCommand> _validator;

    public UpdateProductHandler(IUnitOfWork unit, IValidator<UpdateProductCommand> validator)
    {
        _unit = unit;
        _validator = validator;
    }

    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingProduct = await _unit.QueryRepository<Product>().GetByIdAsync(request.Id);
        if (existingProduct is null) return false;

        existingProduct.Name = request.Name;
        existingProduct.Description = request.Description;
        existingProduct.Price = request.Price;
        existingProduct.ImageUrl = request.ImageUrl;
        existingProduct.Type = request.Type;
        existingProduct.Brand = request.Brand;
        existingProduct.QuantityInStock = request.QuantityInStock;

        _unit.CommandRepository<Product>().Update(existingProduct);
        return await _unit.Complete();
    }
}
