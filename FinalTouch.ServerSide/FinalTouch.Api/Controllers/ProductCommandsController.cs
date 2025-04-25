using FinalTouch.Api.Dtos;
using FinalTouch.Application.Features.Products.Commands;
using FinalTouch.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinalTouch.Api.Controllers;

[Route("api/product")]
[ApiController]
public class ProductCommandsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct([FromBody] CreateProductCommand command)
    {
        var product = await mediator.Send(command);
        return CreatedAtAction(nameof(CreateProduct), new { id = product.Id }, product);
    }



    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductCommand command)
    {
        if (id != command.Id) return BadRequest("Mismatched product ID");

        var result = await mediator.Send(command);
        return result ? NoContent() : NotFound();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var result = await mediator.Send(new DeleteProductCommand(id));
        return result ? NoContent() : NotFound();
    }

}
