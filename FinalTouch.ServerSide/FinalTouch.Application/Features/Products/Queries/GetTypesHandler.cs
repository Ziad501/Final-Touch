using FinalTouch.Core.Entities;
using FinalTouch.Core.Interfaces;
using FinalTouch.Core.Specifications;
using MediatR;

namespace FinalTouch.Application.Features.Products.Queries;

public class GetTypesHandler : IRequestHandler<GetTypesQuery, IReadOnlyList<string>>
{
    private readonly IUnitOfWork _unit;

    public GetTypesHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<IReadOnlyList<string>> Handle(GetTypesQuery request, CancellationToken cancellationToken)
    {
        var spec = new TypeListSpec();
        var result = await _unit.QueryRepository<Product>().ListAsync(spec);
        return result.Cast<string>().ToList();
    }
}
