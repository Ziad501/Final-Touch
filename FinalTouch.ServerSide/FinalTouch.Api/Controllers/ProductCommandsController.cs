using FinalTouch.Api.Dtos;
using FinalTouch.Core.Entities;
using FinalTouch.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinalTouch.Api.Controllers;

[Route("api/product")]
[ApiController]
public class ProductCommandsController(IUnitOfWork unit) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct([FromBody] CreateProductDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            ImageUrl = dto.PictureUrl,
            Type = dto.Type,
            Brand = dto.Brand,
            QuantityInStock = dto.QuantityInStock
        };

        unit.CommandRepository<Product>().Add(product);

        if (await unit.Complete())
        {
            return CreatedAtAction(nameof(CreateProduct), new { id = product.Id }, product);
        }

        return BadRequest("Problem creating product");
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, Product product)
    {
        if (id != product.Id || !unit.CommandRepository<Product>().ProductExists(id))
        {
            return BadRequest("Invalid product ID");
        }

        unit.CommandRepository<Product>().Update(product);
        return await unit.Complete() ? NoContent() : BadRequest("Problem updating product");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await unit.QueryRepository<Product>().GetByIdAsync(id);
        if (product == null) return NotFound();

        unit.CommandRepository<Product>().Delete(product);
        return await unit.Complete() ? NoContent() : BadRequest("Problem deleting product");
    }
}
