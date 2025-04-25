using FinalTouch.Core.Entities;
using FinalTouch.Core.Interfaces;
using MediatR;

namespace FinalTouch.Application.Features.Products.Commands;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IUnitOfWork _unit;

    public DeleteProductHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _unit.QueryRepository<Product>().GetByIdAsync(request.Id);
        if (product is null) return false;

        _unit.CommandRepository<Product>().Delete(product);
        return await _unit.Complete();
    }
}
