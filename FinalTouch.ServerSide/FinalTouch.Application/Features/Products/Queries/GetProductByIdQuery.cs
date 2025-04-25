using FinalTouch.Core.Entities;
using MediatR;

namespace FinalTouch.Application.Features.Products.Queries;

public class GetProductByIdQuery : IRequest<Product?>
{
    public int Id { get; }

    public GetProductByIdQuery(int id)
    {
        Id = id;
    }
}
