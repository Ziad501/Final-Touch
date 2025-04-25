using FinalTouch.Core.Entities;
using FinalTouch.Core.Interfaces;
using FinalTouch.Core.Specifications;
using MediatR;

namespace FinalTouch.Application.Features.Products.Queries;

public class GetBrandsHandler : IRequestHandler<GetBrandsQuery, IReadOnlyList<string>>
{
    private readonly IUnitOfWork _unit;

    public GetBrandsHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<IReadOnlyList<string>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
    {
        var spec = new BrandListSpec();
        var result = await _unit.QueryRepository<Product>().ListAsync(spec);
        return result.Cast<string>().ToList();
    }
}
