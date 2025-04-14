using System;
using FinalTouch.Core.Entities;

namespace FinalTouch.Core.Specifications;

public class TypeListSpec :BaseSpecification<Product, string>
{
    public TypeListSpec()
    {
        AddSelect(x => x.Type);
        ApplyDistinct();
    }
}
