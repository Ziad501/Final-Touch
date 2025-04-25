using FinalTouch.Api.RequestHelpers;
using FinalTouch.Core.Entities;
using FinalTouch.Core.Interfaces;
using FinalTouch.Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace FinalTouch.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductQueriesController(IUnitOfWork unit) : ControllerBase
{
[HttpGet]
public async Task<ActionResult<Pagination<Product>>> GetProducts([FromQuery] ProductSpecParams specParams)
{
    var countSpec = new ProductSpecification(specParams, isPagingEnabled: false);
    var totalItems = await unit.QueryRepository<Product>().CountAsync(countSpec);

    var dataSpec = new ProductSpecification(specParams, isPagingEnabled: true);
    var products = await unit.QueryRepository<Product>().ListAsync(dataSpec);

    return Ok(new Pagination<Product>(
        pageIndex: specParams.PageIndex,
        pageSize: specParams.PageSize,
        count: totalItems,
        data: products
));
}

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await unit.QueryRepository<Product>().GetByIdAsync(id);
        return product is null ? NotFound() : Ok(product);
    }

    [HttpGet("type")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
    {
        var spec = new TypeListSpec();
        var result = await unit.QueryRepository<Product>().ListAsync(spec);
        return Ok(result.Cast<string>().ToList());
    }

    [HttpGet("brand")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
    {
        var spec = new BrandListSpec();
        var result = await unit.QueryRepository<Product>().ListAsync(spec);
        return Ok(result.Cast<string>().ToList());
    }
}
