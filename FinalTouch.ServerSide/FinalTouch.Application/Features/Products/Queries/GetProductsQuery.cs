using FinalTouch.Application.Common;
using FinalTouch.Core.Entities;
using FinalTouch.Core.Specifications;
using MediatR;

namespace FinalTouch.Application.Features.Products.Queries;

public class GetProductsQuery : IRequest<Pagination<Product>>
{
    public ProductSpecParams SpecParams { get; set; }

    public GetProductsQuery(ProductSpecParams specParams)
    {
        SpecParams = specParams;
    }
}
