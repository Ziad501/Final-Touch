using FinalTouch.Core.Entities;
using FinalTouch.Core.Interfaces;
using FinalTouch.Core.Specifications;
using MediatR;
using FinalTouch.Application.Common;

namespace FinalTouch.Application.Features.Products.Queries;

public class GetProductsHandler : IRequestHandler<GetProductsQuery, Pagination<Product>>
{
    private readonly IUnitOfWork _unit;

    public GetProductsHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<Pagination<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var spec = new ProductSpecification(request.SpecParams, isPagingEnabled: true);
        var countSpec = new ProductSpecification(request.SpecParams, isPagingEnabled: false);

        var totalItems = await _unit.QueryRepository<Product>().CountAsync(countSpec);
        var products = await _unit.QueryRepository<Product>().ListAsync(spec);

        return new Pagination<Product>(
            request.SpecParams.PageIndex,
            request.SpecParams.PageSize,
            totalItems,
            products
        );
    }
}
