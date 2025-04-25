using FinalTouch.Application.Features.Products.Queries;
using FinalTouch.Core.Entities;
using FinalTouch.Core.Specifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinalTouch.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductQueriesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetProducts([FromQuery] ProductSpecParams specParams)
    {
        var result = await mediator.Send(new GetProductsQuery(specParams));
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await mediator.Send(new GetProductByIdQuery(id));
        return product is null ? NotFound() : Ok(product);
    }

    [HttpGet("brand")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
    {
        var result = await mediator.Send(new GetBrandsQuery());
        return Ok(result);
    }

    [HttpGet("type")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
    {
        var result = await mediator.Send(new GetTypesQuery());
        return Ok(result);
    }
}
