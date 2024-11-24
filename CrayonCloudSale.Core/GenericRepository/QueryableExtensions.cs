using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace CrayonCloudSale.Infrastructure.GenericRepository;
public static class QueryableExtensions
{
    public static IQueryable<TEntity> AddWhere<TEntity>(this IQueryable<TEntity> query, Expression<Func<TEntity, bool>>? filter)
        where TEntity : class
    {
        if (filter != null)
        {
            query = query.Where(filter);
        }

        return query;
    }

    public static IQueryable<TEntity> AddInclude<TEntity>(this IQueryable<TEntity> query, params Expression<Func<TEntity, object>>?[] includeProperties)
        where TEntity : class
    {
        foreach (var include in includeProperties)
        {
            if (include is null)
            {
                continue;
            }

            query = query.Include(include);
        }

        return query;
    }
}
