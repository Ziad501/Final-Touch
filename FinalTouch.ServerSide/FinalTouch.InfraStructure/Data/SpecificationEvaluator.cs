using FinalTouch.Core.Entities;
using FinalTouch.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinalTouch.InfraStructure.Data;

public class SpecificationEvaluator<T> where T : BaseEntity
{
    public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> spec)
    {
        if (spec.Criteria != null)
            query = query.Where(spec.Criteria);

        if (spec.OrderBy != null)
            query = query.OrderBy(spec.OrderBy);

        if (spec.OrderByDescending != null)
            query = query.OrderByDescending(spec.OrderByDescending);

        query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
        query = spec.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));

        if (spec.IsDistinct)
            query = query.Distinct();

        if (spec.IsPagingEnabled)
            query = query.Skip(spec.Skip).Take(spec.Take);

        return query;
    }

    public static IQueryable<TResult> GetQuery<TResult>(
        IQueryable<T> query,
        ISpecification<T, TResult> spec)
    {
        if (spec.Criteria != null)
            query = query.Where(spec.Criteria);

        if (spec.OrderBy != null)
            query = query.OrderBy(spec.OrderBy);

        if (spec.OrderByDescending != null)
            query = query.OrderByDescending(spec.OrderByDescending);

        query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
        query = spec.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));

        if (spec.Select == null)
            throw new InvalidOperationException("Select expression must be provided.");

        var projected = query.Select(spec.Select);

        if (spec.IsDistinct)
            projected = projected.Distinct();

        if (spec.IsPagingEnabled)
            projected = projected.Skip(spec.Skip).Take(spec.Take);

        return projected ?? query.Cast<TResult>();
    }
}
