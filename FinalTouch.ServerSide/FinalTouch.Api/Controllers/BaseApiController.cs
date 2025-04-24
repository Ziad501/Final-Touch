using FinalTouch.Core.Entities;
using FinalTouch.Core.Interfaces;
using FinalTouch.Api.RequestHelpers;
using Microsoft.AspNetCore.Mvc;

namespace FinalTouch.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        protected async Task<ActionResult> CreatePagedResult<T>(
            IProductCommandRepository<T> cmd,
            IProductQueryRepository<T> quer,
            IUnitOfWork unit,
            ISpecification<T> spec,
            int pageIndex,
            int pageSize) where T : BaseEntity
        {
            var (items, count) = await GetPaginatedData(quer, spec);
            var pagination = new Pagination<T>(pageIndex, pageSize, count, items);

            return Ok(pagination);
        }

<<<<<<< HEAD
        protected async Task<ActionResult> CreatePagedResult<T, TDto>(
            IProductCommandRepository<T> cmd,
            IProductQueryRepository<T> quer,
            IUnitOfWork unit,
            ISpecification<T> spec,
            int pageIndex,
            int pageSize,
            Func<T, TDto> toDto) where T : BaseEntity
        {
            var (items, count) = await GetPaginatedData(quer, spec);
            var dtoItems = items.Select(toDto).ToList();
            var pagination = new Pagination<TDto>(pageIndex, pageSize, count, dtoItems);

            return Ok(pagination);
        }

        private static async Task<(IReadOnlyList<T> items, int count)> GetPaginatedData<T>(
            IProductQueryRepository<T> quer,
            ISpecification<T> spec) where T : BaseEntity
        {
            var items = await quer.ListAsync(spec);
            var count = await quer.CountAsync(spec);
            return (items, count);
        }
    }
=======
			return Ok(pagination);
		}
		protected async Task<ActionResult> CreatePagedResult<T, TDto>(IGenericRepository<T> repo,
			ISpecification<T> spec, int pageIndex, int pageSize, Func<T, TDto> toDto) where T
			: BaseEntity, IDtoConvertible
		{
			var items = await repo.ListAsync(spec);
			var count = await repo.CountAsync(spec);

			var dtoItems = items.Select(toDto).ToList();

			var pagination = new Pagination<TDto>(pageIndex, pageSize, count, dtoItems);

			return Ok(pagination);
		}
	}
>>>>>>> origin/main
}
