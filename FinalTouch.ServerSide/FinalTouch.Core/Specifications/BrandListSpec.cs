using System;
using FinalTouch.Core.Entities;

namespace FinalTouch.Core.Specifications;

public class BrandListSpec : BaseSpecification<Product, string>
{
    public BrandListSpec()
    {
        AddSelect(x => x.Brand);
        ApplyDistinct();
    }
}
