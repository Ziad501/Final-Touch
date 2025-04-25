using FinalTouch.Core.Entities;
using FinalTouch.Core.Interfaces;
using MediatR;

namespace FinalTouch.Application.Features.Products.Queries;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Product?>
{
    private readonly IUnitOfWork _unit;

    public GetProductByIdHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        return await _unit.QueryRepository<Product>().GetByIdAsync(request.Id);
    }
}
