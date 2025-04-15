using FinalTouch.Api.RequestHelpers;
using FinalTouch.Core.Entities;
using FinalTouch.Core.Interfaces;
using FinalTouch.Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace FinalTouch.Api.Controllers
{
    public class ProductController(IGenericRepository<Product> repo) : BaseApiController
    {

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery] ProductSpecParams specParams)
        {
            var spec = new ProductSpecification(specParams);

            
			return await CreatePagedResult(repo, spec,specParams.PageIndex,specParams.PageSize);
            
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            Product? product = await repo.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProdcut(Product product)
        {
            repo.Add(product);
            if(await repo.SaveChangesAsync())
            {
                return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
            }
            return BadRequest("problem creating product");
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, Product product)
        {
            if(product.Id != id || !ProductExists(id))
            {
                return BadRequest("cannot update product with this id");
            }
            repo.Update(product);
            if(await repo.SaveChangesAsync()) {return NoContent();}
            
            return BadRequest("problem updating product");
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await repo.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            repo.Delete(product);
             if(await repo.SaveChangesAsync()) {return NoContent();}

            return BadRequest("problem deleting product");
        }
        [HttpGet("type")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            var spec = new TypeListSpec();
            return Ok(await repo.ListAsync(spec));
        }
        [HttpGet("brand")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            var spec = new BrandListSpec();
            return Ok(await repo.ListAsync(spec));
        }
        private bool ProductExists(int id)
        {
            return repo.ProductExists(id);
        }
    }
}
