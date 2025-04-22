using API.RequestHelpers;
using FinalTouch.Api.RequestHelpers;
using FinalTouch.Core.Entities;
using FinalTouch.Core.Interfaces;
using FinalTouch.Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalTouch.Api.Controllers
{
    public class ProductController(IUnitOfWork unit) : BaseApiController
    {
        [Cache(600)]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery] ProductSpecParams specParams)
        {
            var spec = new ProductSpecification(specParams);

            
			return await CreatePagedResult(unit.Repository<Product>(), spec,specParams.PageIndex,specParams.PageSize);
            
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            Product? product = await unit.Repository<Product>().GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }
        [InvalidateCache("api/products|")]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProdcut(Product product)
        {
            unit.Repository<Product>().Add(product);
            if(await unit.Complete())
            {
                return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
            }
            return BadRequest("problem creating product");
        }
		[InvalidateCache("api/products|")]
		[Authorize(Roles = "Admin")]
		[HttpPut("{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, Product product)
        {
            if(product.Id != id || !ProductExists(id))
            {
                return BadRequest("cannot update product with this id");
            }
            unit.Repository<Product>().Update(product);
            if(await unit.Complete()) {return NoContent();}
            
            return BadRequest("problem updating product");
        }
		[InvalidateCache("api/products|")]
		[Authorize(Roles = "Admin")]
		[HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await unit.Repository<Product>().GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            unit.Repository<Product>().Delete(product);
             if(await unit.Complete()) {return NoContent();}

            return BadRequest("problem deleting product");
        }
		[Cache(1000)]
		[HttpGet("type")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            var spec = new TypeListSpec();
            return Ok(await unit.Repository<Product>().ListAsync(spec));
        }
		[Cache(1000)]
		[HttpGet("brand")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            var spec = new BrandListSpec();
            return Ok(await unit.Repository<Product>().ListAsync(spec));
        }
        private bool ProductExists(int id)
        {
            return unit.Repository<Product>().ProductExists(id);
        }
    }
}
