using FinalTouch.Api.RequestHelpers;
using FinalTouch.Core.Entities;
using FinalTouch.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinalTouch.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class BaseApiController : ControllerBase
	{
		protected async Task<ActionResult> CreatePagedResult<T>(IGenericRepository<T> repo, 
			ISpecification<T> spec, int pageIndex, int pageSize) where T : BaseEntity
		{
			var items = await repo.ListAsync(spec);
			var count = await repo.CountAsync(spec);

			var pagination = new Pagination<T>(pageIndex, pageSize, count, items);

			return Ok(pagination);
		}
	}
}
