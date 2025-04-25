using MediatR;

namespace FinalTouch.Application.Features.Products.Commands;

public class DeleteProductCommand : IRequest<bool>
{
    public int Id { get; }

    public DeleteProductCommand(int id)
    {
        Id = id;
    }
}
