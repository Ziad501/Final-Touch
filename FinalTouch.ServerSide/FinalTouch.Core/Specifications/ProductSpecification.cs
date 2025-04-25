using System;
using FinalTouch.Core.Entities;

namespace FinalTouch.Core.Specifications;

public class ProductSpecification : BaseSpecification<Product>
{
    public ProductSpecification(ProductSpecParams specParams, bool isPagingEnabled = true)
        : base(x =>
            (string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search)) &&
            (!specParams.Brands.Any() || specParams.Brands.Contains(x.Brand)) &&
            (!specParams.Types.Any() || specParams.Types.Contains(x.Type)))
    {
        // Apply sorting
        switch (specParams.Sort)
        {
            case "price":
                AddOrderBy(x => x.Price);
                break;
            case "priceDesc":
                AddOrderByDescending(x => x.Price);
                break;
            default:
                AddOrderBy(x => x.Name);
                break;
        }

        // Apply paging only if enabled
        if (isPagingEnabled)
        {
            ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
		}
	}
}
