using MediatR;

namespace FinalTouch.Application.Features.Products.Queries;

public class GetBrandsQuery : IRequest<IReadOnlyList<string>> { }
