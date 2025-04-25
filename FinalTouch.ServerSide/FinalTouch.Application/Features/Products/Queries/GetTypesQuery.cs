using MediatR;
using System.Collections.Generic;

namespace FinalTouch.Application.Features.Products.Queries;

public class GetTypesQuery : IRequest<IReadOnlyList<string>> { }
