using System;
using FinalTouch.Core.Entities;
using FinalTouch.Core.Interfaces;

namespace FinalTouch.InfraStructure.Data;

public class SpecificationEvaluator<T> where T : BaseEntity
{
    public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> spec)
    {
        
        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria);
        }
        if(spec.OrderBy != null)
        {
            query = query.OrderBy(spec.OrderBy);
        }
        if(spec.OrderByDescending != null)
        {
            query = query.OrderByDescending(spec.OrderByDescending);
        }
        if(spec.IsDistinct)
        {
            query = query.Distinct();
        }
        if (spec.IsPagingEnabled)
		{
			query = query.Skip(spec.Skip).Take(spec.Take);
		}
		return query;
    }
    public static IQueryable<TResult> GetQuery<TSpec,TResult>(IQueryable<T> query, ISpecification<T,TResult> spec)
    {
        
        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria);
        }
        if(spec.OrderBy != null)
        {
            query = query.OrderBy(spec.OrderBy);
        }
        if(spec.OrderByDescending != null)
        {
            query = query.OrderByDescending(spec.OrderByDescending);
        }
        var selectQuery = query as IQueryable<TResult>;
        if (spec.Select != null)
        {
            selectQuery = query.Select(spec.Select);
        }
        if(spec.IsDistinct)
        {
            selectQuery = selectQuery?.Distinct();
        }
		if (spec.IsPagingEnabled)
		{
			selectQuery = selectQuery?.Skip(spec.Skip).Take(spec.Take);
		}
		// If selectQuery is null, it means the query was not casted to IQueryable<TResult>, so we return the original query casted to TResult
		// Otherwise, we return the selectQuery
		return selectQuery ?? query.Cast<TResult>();
        
    }

}
